using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    public class ConsultorioTest
    {
        [Fact]
        public void Constructor_NombreNulo_LanzaExcepcion()
        {
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Consultorio(null!));
        }

        [Fact]
        public void Constructor_NombreVacio_LanzaExcepcion()
        {
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Consultorio(string.Empty));
        }

        [Fact]
        public void Constructor_ConNombreValido_CreaConsultorio()
        {
            var nombre = "Consultorio Central";
            var consultorio = new Consultorio(nombre);

            Assert.NotNull(consultorio);
            Assert.Equal(nombre, consultorio.Nombre);
        }
    }
}