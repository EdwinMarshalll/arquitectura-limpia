using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;

// El IRequest nos sirve para enviarlo por el mediador y el Guid de tipo de respuesta
public class ComandoCrearConsultorio : IRequest<Guid>
{
    public required string Nombre { get; set; }
}
