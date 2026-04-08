using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace DientesLimpios.Pruebas.Aplicacion.CasosDeUso.Consultorios;

public class CasoDeUsoCrearConsultorioTest
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private IRepositorioConsultorios repositorio;
    private IUnidadDeTrabajo unidadDeTrabajo;
    private CasoDeUsoCrearConsultorio casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [Fact]
    public async Task Handle_ComandoValido_ObtenemosIdConsultorio()
    {
        // Arrange
        repositorio = Substitute.For<IRepositorioConsultorios>();
        unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
        casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo);

        var comando = new ComandoCrearConsultorio() { Nombre = "Consultorio A" };

        var consultorioCreado = new Consultorio("Consultorio A");
        repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

        // Act
        var resultado = await casoDeUso.Handle(comando);
        
        // Assert
        await repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
        await unidadDeTrabajo.Received(1).Persistir();

        Assert.NotEqual(Guid.Empty, resultado);
    }

    [Fact]
    public async Task Handle_CuandoHayError_HacerRollback()
    {
        // Arrange
        repositorio = Substitute.For<IRepositorioConsultorios>();
        unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
        casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo);

        var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
        repositorio.Agregar(Arg.Any<Consultorio>()).ThrowsAsync(new Exception("Error DB"));

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await casoDeUso.Handle(comando);
        });

        // Verificaciones
        await unidadDeTrabajo.Received(1).Revertir();
        await unidadDeTrabajo.DidNotReceive().Persistir();
    }
}
