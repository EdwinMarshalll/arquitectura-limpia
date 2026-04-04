using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    public class PacienteTest
    {
        [Fact]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            string nombre = null!;
            Email email = new("edwin@gmail.com");
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Paciente(nombre, email));
        }

        [Fact]
        public void Constructor_NombreVacio_LanzaExcepcion()
        {
            string nombre = string.Empty;
            Email email = new("edwin@gmail.com");

            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Paciente(nombre, email));
        }

        [Fact]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            string nombre = "Edwin";
            Email email = null!;

            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Paciente(nombre, email));
        }

        [Fact]
        public void Constructor_ConDatosValidos_CreaPaciente()
        {
            string nombre = "Edwin";
            Email email = new("edwin@gmail.com");

            var paciente = new Paciente(nombre, email);

            Assert.NotNull(paciente);
            Assert.IsType<Guid>(paciente.Id);
        }
    }
}
