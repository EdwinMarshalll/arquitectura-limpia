using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CasoDeUsoCrearConsultorio
    {
        private readonly IRepositorioConsultorios repositorio;

        public CasoDeUsoCrearConsultorio(IRepositorioConsultorios repositorio)
        {
            this.repositorio = repositorio;
        }

        public async Task<Guid> Handle(ComandoCrearConsultorio comando)
        {
            var consultorio = new Consultorio(comando.Nombre);
            var respuesta = await repositorio.Agregar(consultorio);
            return respuesta.Id;
        }
    }
}
