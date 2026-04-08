using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia
{
    public class DientesLimpiosDbContext : DbContext
    {
        public DientesLimpiosDbContext(DbContextOptions<DientesLimpiosDbContext> options) : base(options)
        {
            
        }

        protected DientesLimpiosDbContext()
        {

        }

        public DbSet<Consultorio> MyProperty { get; set; }
    }
}
