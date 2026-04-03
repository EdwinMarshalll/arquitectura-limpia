using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Consultorio
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; } = null!;

        public Consultorio(string nombre) {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }

            Nombre = nombre;
            Id = Guid.CreateVersion7(); // Hace que el Id sea único y ordenado cronológicamente, lo que es útil para bases de datos y sistemas distribuidos. ya que no rompe la secuencia de generación de Ids, a diferencia de los GUIDs tradicionales (versión 4) que son completamente aleatorios.
        }
    }
}
