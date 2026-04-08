using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public interface IMediator
    {
        // Devolvemos un TResponse (lo que sea)
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
    }
}
