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
            // Quiero crear dinamicamente un tipo IRequestHandler que tenga como primer parametro generico un IRequest y como segundo tipo generico un TResponse
            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso);
            if(casoDeUso is null)// Significa que no hemos registrado el caso de uso
            {
                throw new Exception($"No se encontro un handler para {request.GetType().Name}");
            }

            // El metodo a ejecutar se llama Handle
            var metodo = tipoCasoDeUso.GetMethod("Handle")!;

            return await (Task<TResponse>)metodo.Invoke(casoDeUso, new object[] { request })!;
        }
    }
}
