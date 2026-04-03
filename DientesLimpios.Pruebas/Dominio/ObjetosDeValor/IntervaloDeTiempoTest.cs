using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{
    public class IntervaloDeTiempoTest
    {
        [Fact]
        public void Constructor_FechaInicioMayorAFechaFin_LanzaExcepcion()
        {
            DateTime fechaInicio = DateTime.UtcNow.AddDays(10);
            DateTime fechaFin = DateTime.UtcNow;

            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => new IntervaloDeTiempo(fechaInicio, fechaFin));
        }

        [Fact]
        public void Constructor_FechaInicioMenorAFechaFin_CreaIntervaloDeTiempo()
        {
            DateTime fechaInicio = DateTime.UtcNow;
            DateTime fechaFin = DateTime.UtcNow.AddMinutes(30);

            var intervalo = new IntervaloDeTiempo(fechaInicio, fechaFin);

            Assert.NotNull(intervalo);
            Assert.True(intervalo.Inicio < intervalo.Fin);
        }
    }
}