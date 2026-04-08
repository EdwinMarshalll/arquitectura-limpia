using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia
{
    public static class RegistroDeServiciosDePersistencia
    {
        public static IServiceCollection AgregarServiciosDePersistencia(this IServiceCollection services)
        {
            services.AddDbContext<DientesLimpiosDbContext>(options =>
            {
                options.UseSqlServer("name=DientesLimpiosConnectionString");
            });


            return services;
        }
    }
}
