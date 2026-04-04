using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    public class DentistaTest
    {
        [Fact]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            var email = new Email("edwin@gmail.com");
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Dentista(null!, email ));
        }

        [Fact]
        public void Constructor_NombreVacio_LanzaExcepcion()
        {
            var email = new Email("edwin@gmail.com");
            var nombre = string.Empty;

            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Dentista(nombre, email));
        }

        [Fact]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            var nombre = "Edwin";
            Email email = null!;

            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Dentista(nombre, email));
        }

        [Fact]
        public void Constructor_ConDatosValidos_CreaDentista()
        {
            Email email = new("edwin@gmail.com");
            string nombre = "Edwin";

            var dentista = new Dentista(nombre, email);
           
            Assert.NotNull(dentista);
            Assert.IsType<Guid>(dentista.Id);
        }
    }
}
