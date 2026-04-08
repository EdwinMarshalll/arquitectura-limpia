using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using FluentValidation;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.Utilidades.Mediador
{
    public class MediadorSimpleTests
    {
        public class RequestFalso : IRequest<string> {
            public required string Nombre { get; set; }
        }

        public class HandlerFalso : IRequestHandler<RequestFalso, string> { 
            public Task<string> Handle(RequestFalso request)
            {
                return Task.FromResult("Respuesta correcta");
            }
        }

        public class ValidadorRequestFalso : AbstractValidator<RequestFalso>
        {
            public ValidadorRequestFalso()
            {
                RuleFor(p => p.Nombre).NotEmpty();
            }
        }

        [Fact]
        public async Task Send_LlamaMetodoHandler()
        {
            var request = new RequestFalso() { Nombre  = "A"};

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();
            serviceProvider
                .GetService(typeof(IRequestHandler<RequestFalso, string>))
                .Returns(casoDeUsoMock);

            var mediador = new MediadorSimple(serviceProvider);

            var resultado = await mediador.Send(request);

            await casoDeUsoMock.Received(1).Handle(request);
        }

        [Fact]
        public async Task Send_SinHandlerRegistrado_LanzaExcepcion()
        {
            var request = new RequestFalso() { Nombre = "A" };

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();

            var mediador = new MediadorSimple(serviceProvider);

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var resultado = await mediador.Send(request);
            });
        }

        [Fact]
        public async Task Send_ComandoNoValido_LanzaExcepcion()
        {
            var request = new RequestFalso { Nombre = "" };
            var serviceProvider = Substitute.For<IServiceProvider>();
            var validador = new ValidadorRequestFalso();

            serviceProvider
                .GetService(typeof(IValidator<RequestFalso>))
                .Returns(validador);

            var mediador = new MediadorSimple(serviceProvider);

            await Assert.ThrowsAsync<ExcepcionDeValidacion>(async () =>
            {
                await mediador.Send(request);
            });

        }
    }
}
