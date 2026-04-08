using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.DetalleConsultorio;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios
{
    public class CasoDeUsoObtenerDetalleConsultorioTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioConsultorios repositorio;
        private CasoDeUsoObtenerDetalleConsultorio casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [Fact]
        public async Task Handle_ConsultorioExiste_RetornarDto()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerDetalleConsultorio(repositorio);

            // Arrange
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            repositorio.ObtenerPorId(id).Returns(consultorio);

            // Act
            var resultado = await casoDeUso.Handle(consulta);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.Id);
            Assert.Equal("Consultorio A", resultado.Nombre);
        }

        [Fact]
        public async Task Handle_ConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            casoDeUso = new CasoDeUsoObtenerDetalleConsultorio(repositorio);

            // Arrange
            var id = Guid.NewGuid();
            var consulta = new ConsultaObtenerDetalleConsultorio { Id = id };

            repositorio.ObtenerPorId(id).ReturnsNull();

            // Act
            // Assert
            await Assert.ThrowsAsync<ExcepcionNoEncontrado>(async () =>
            {
               await casoDeUso.Handle(consulta);
            });
        }


    }
}
