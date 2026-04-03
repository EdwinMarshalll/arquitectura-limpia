using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{
    public class EmailTest
    {
        [Fact]
        public void Constructor_EmailNulo_LanzaExcepcion()
        {
            // Vamos a probar que el constructor de la clase Email lance una ExcepcionDeReglaDeNegocio cuando se le pase un valor nulo.
            Assert.Throws<ExcepcionDeReglaDeNegocio>(
                () => new Email(null!)
            );
        }

        [Fact]
        public void Constructor_EmailSinArroba_LanzaExcepcion()
        {
            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new Email("edwin.com"));
        }

        [Fact]
        public void Constructor_EmailValido_CreaInstancia()
        {
            var email = new Email("edwin@gmail.com");

            Assert.NotNull(email);
            Assert.Equal("edwin@gmail.com", email.Valor);
        }

    }
}
