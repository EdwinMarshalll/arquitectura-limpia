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

    public CasoDeUsoCrearConsultorio(IRepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo)
    {
        this.repositorio = repositorio;
        this.unidadDeTrabajo = unidadDeTrabajo;
    }

    public async Task<Guid> Handle(ComandoCrearConsultorio comando)
    {
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
