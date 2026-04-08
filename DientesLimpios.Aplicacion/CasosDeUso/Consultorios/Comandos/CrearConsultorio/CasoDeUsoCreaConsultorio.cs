using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;

public class CasoDeUsoCrearConsultorio : IRequestHandler<ComandoCrearConsultorio, Guid>
{
    private readonly IRepositorioConsultorios repositorio;
    private readonly IUnidadDeTrabajo unidadDeTrabajo;
    private readonly IValidator<ComandoCrearConsultorio> validador;

    public CasoDeUsoCrearConsultorio(IRepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo, IValidator<ComandoCrearConsultorio> validador)
    {
        this.repositorio = repositorio;
        this.unidadDeTrabajo = unidadDeTrabajo;
        this.validador = validador;
    }

    public async Task<Guid> Handle(ComandoCrearConsultorio comando)
    {
        var resultadoValidacion = await validador.ValidateAsync(comando);
        if (!resultadoValidacion.IsValid)
        {
            // Lanzar excepcion personalizada con el listado de errores de fluent Validation
            throw new ExcepcionDeValidacion(resultadoValidacion);
        }

        var consultorio = new Consultorio(comando.Nombre);
        try
        {
            var respuesta = await repositorio.Agregar(consultorio);
            await unidadDeTrabajo.Persistir();
            return respuesta.Id;
        }
        catch (Exception)
        {
            await unidadDeTrabajo.Revertir();
            throw; // lanzamos la excepcion para que una capa superior atrape la excepcion y la procese
        }
    }
}
