using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Persistencia.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IRepositorioConsultorios, RepositorioConsultorios>();

            return services;
        }
    }
}
