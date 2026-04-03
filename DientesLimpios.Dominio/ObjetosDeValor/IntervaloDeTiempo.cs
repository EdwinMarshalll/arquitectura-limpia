namespace DientesLimpios.Dominio.ObjetosDeValor
{
    // Record hace que la clase sea inmutable y tenga igualdad basada en el valor de sus propiedades, lo que es ideal para objetos de valor.
    public record IntervaloDeTiempo
    {
        public DateTime Inicio { get; }
        public DateTime Fin { get; }

        public IntervaloDeTiempo(DateTime inicio, DateTime fin)
        {
            if (inicio >= fin)
            {
                throw new Excepciones.ExcepcionDeReglaDeNegocio("La fecha de inicio no puede ser mayor a la fecha de fin.");
            }

            Inicio = inicio;
            Fin = fin;
        }
    }
}
