using DientesLimpios.Aplicacion.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public class MediadorSimple : IMediator
    {
        private readonly IServiceProvider serviceProvider;

        // El IServiceProvider nos sirve para  poder tomar servicios del proveedor de inyeccion de dependencias
        public MediadorSimple(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType());
            var validador = serviceProvider.GetService(tipoValidador);
            if (validador is not null)
            {
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");
                var tareaValidar = (Task)metodoValidar!.Invoke(validador, new object[] { request, CancellationToken.None })!;

                await tareaValidar.ConfigureAwait(false);

                var resultado = tareaValidar.GetType().GetProperty("Result");
                var validationResult = (ValidationResult)resultado!.GetValue(tareaValidar)!;

                if (!validationResult.IsValid) {
                    throw new ExcepcionDeValidacion(validationResult);
                }
            }


            // Quiero crear dinamicamente un tipo IRequestHandler que tenga como primer parametro generico un IRequest y como segundo tipo generico un TResponse
            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso);
            if (casoDeUso is null)// Significa que no hemos registrado el caso de uso
            {
                throw new Exception($"No se encontro un handler para {request.GetType().Name}");
            }

            // El metodo a ejecutar se llama Handle
            var metodo = tipoCasoDeUso.GetMethod("Handle")!;

            return await (Task<TResponse>)metodo.Invoke(casoDeUso, new object[] { request })!;
        }
    }
}
