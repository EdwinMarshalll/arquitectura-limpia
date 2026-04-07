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
    private IValidator<ComandoCrearConsultorio> validador;
    private CasoDeUsoCrearConsultorio casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    [Fact]
    public async Task Handle_ComandoValido_ObtenemosIdConsultorio()
    {
        // Arrange
        repositorio = Substitute.For<IRepositorioConsultorios>();
        unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
        validador = Substitute.For<IValidator<ComandoCrearConsultorio>>();
        casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo, validador);

        var comando = new ComandoCrearConsultorio() { Nombre = "Consultorio A" };
        validador.ValidateAsync(comando)
            .Returns(new ValidationResult());

        var consultorioCreado = new Consultorio("Consultorio A");
        repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);

        // Act
        var resultado = await casoDeUso.Handle(comando);
        
        // Assert
        await validador.Received(1).ValidateAsync(comando);
        await repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
        await unidadDeTrabajo.Received(1).Persistir();

        Assert.NotEqual(Guid.Empty, resultado);
    }

    [Fact]
    public async Task Handle_ComandoNoValido_LanzaExcepcion()
    {
        // Arrange
        repositorio = Substitute.For<IRepositorioConsultorios>();
        unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
        validador = Substitute.For<IValidator<ComandoCrearConsultorio>>();
        casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo, validador);

        var comando = new ComandoCrearConsultorio { Nombre = "" };
        var resultadoInvalido = new ValidationResult(new[] {
            new ValidationFailure("Nombre", "El nombre es obligatorio"),
        });

        validador.ValidateAsync(comando).Returns(resultadoInvalido);

        // Act & Assert

        // Valida que se lance la excepcion
        await Assert.ThrowsAsync<ExcepcionDeValidacion>(async () =>
        {
            await casoDeUso.Handle(comando);
        });

        // Validamos que nunca se invoco el metodo Agregar
        await repositorio.DidNotReceive().Agregar(Arg.Any<Consultorio>());
        // Validar que no se invoco que el metodo de persistir  ni Revertir
        await unidadDeTrabajo.DidNotReceive().Persistir();
        await unidadDeTrabajo.DidNotReceive().Revertir();
    }

    [Fact]
    public async Task Handle_CuandoHayError_HacerRollback()
    {
        // Arrange
        repositorio = Substitute.For<IRepositorioConsultorios>();
        unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
        validador = Substitute.For<IValidator<ComandoCrearConsultorio>>();
        casoDeUso = new CasoDeUsoCrearConsultorio(repositorio, unidadDeTrabajo, validador);

        var comando = new ComandoCrearConsultorio { Nombre = "Consultorio A" };
        validador.ValidateAsync(comando).Returns(new ValidationResult());
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
