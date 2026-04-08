using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    // TRequest es una peticion, ejemplo un comando
    // TResponse es el tipo de salida.
    public interface IRequestHandler<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }
}
