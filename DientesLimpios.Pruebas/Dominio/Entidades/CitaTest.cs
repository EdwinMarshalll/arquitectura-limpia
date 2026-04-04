using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    public class CitaTest
    {
        private Guid _pacienteId = Guid.NewGuid();
        private Guid _dentistaId = Guid.NewGuid();
        private Guid _consultorioId = Guid.NewGuid();
        private IntervaloDeTiempo _intervalo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

        [Fact]
        public void Constructor_FechaInicioMenorAHoy_LanzaExcepcion()
        {
            IntervaloDeTiempo intervalo = new IntervaloDeTiempo(
                DateTime.UtcNow.AddDays(-2), DateTime.UtcNow);

            Assert.Throws<ExcepcionDeReglaDeNegocio>(() =>
                new Cita(
                    _pacienteId,
                    _dentistaId,
                    _consultorioId,
                    intervalo
                )
            );
        }

        [Fact]
        public void Constructor_DatosValidos_CreaCita()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            Assert.Equal(_pacienteId, cita.PacienteId);
            Assert.Equal(_dentistaId, cita.DentistaId);
            Assert.Equal(_consultorioId, cita.ConsultorioId);
            Assert.Equal(_intervalo, cita.IntervaloDeTiempo);
            Assert.Equal(EstadoCita.Programada, cita.Estado);
            Assert.NotEqual(Guid.Empty, cita.Id);
        }

        [Fact]
        public void Cancelar_CitaNoProgramada_LanzaExcepcion()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            cita.Completar();

            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => cita.Cancelar());
        }

        [Fact]
        public void Cancelar_CitaProgramada_CambiaEstadoACancelada()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            cita.Cancelar();

            Assert.Equal(EstadoCita.Cancelada, cita.Estado);
        }

        [Fact]
        public void Completar_CitaNoProgramada_LanzaExcepcion()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            cita.Cancelar();

            Assert.Throws<ExcepcionDeReglaDeNegocio>(() => cita.Completar());
        }

        [Fact]
        public void Completar_CitaProgramada_CambiaEstadoACompletada()
        {
            var cita = new Cita(
                _pacienteId,
                _dentistaId,
                _consultorioId,
                _intervalo
            );

            cita.Completar();

            Assert.Equal(EstadoCita.Completada, cita.Estado);
        }
    }
}
