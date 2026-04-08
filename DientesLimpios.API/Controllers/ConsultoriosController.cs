using DientesLimpios.API.DTOs.Consultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.AspNetCore.Mvc;

namespace DientesLimpios.API.Controllers
{
    [ApiController]
    [Route("api/consultorios")]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IMediator mediator;

        public ConsultoriosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearConsultorioDTO crearConsultorioDTO)
        {
            var comando = new ComandoCrearConsultorio { Nombre = crearConsultorioDTO.Nombre };
            await mediator.Send(comando);
            return Ok();
        }
    }
}
