using DientesLimpios.Dominio.Excepciones;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    public record Email // record Hace que la clase sea inmutable
    {
        public string Valor { get; } = null!;

        public Email( string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }

            if (!email.Contains('@'))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} no es un email válido");
            }

            Valor = email;
        }
    }
}
