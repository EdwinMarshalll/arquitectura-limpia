using DientesLimpios.Aplicacion.Utilidades.Mediador;
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
        public class RequestFalso : IRequest<string> { }

        public class HandlerFalso : IRequestHandler<RequestFalso, string> { 
            public Task<string> Handle(RequestFalso request)
            {
                return Task.FromResult("Respuesta correcta");
            }
        }

        [Fact]
        public async Task Send_LlamaMetodoHandler()
        {
            var request = new RequestFalso();

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
            var request = new RequestFalso();

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProvider = Substitute.For<IServiceProvider>();

            var mediador = new MediadorSimple(serviceProvider);

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var resultado = await mediador.Send(request);
            });
        }
    }
}
