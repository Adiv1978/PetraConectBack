namespace PetraConectBack.Types.Utility
{
    public class ClientException : Exception
    {
        public string? Codigo { get; set; }

        public ClientException()
        {
        }

        public ClientException(string message)
            : base(message)
        {
        }

        public ClientException(string message, string codigo)
            : base(message)
        {
            Codigo = codigo;
        }

        public ClientException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ClientException(string message, string codigo, Exception innerException)
            : base(message, innerException)
        {
            Codigo = codigo;
        }
    }
}
