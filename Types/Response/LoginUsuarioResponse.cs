namespace PetraConectBack.Types.Response
{
    public class LoginUsuarioResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public long? IdUsuario { get; set; }
        public long? IdSesion { get; set; }
        public string? SessionToken { get; set; }
        public DateTime? FecCaduca { get; set; }
    }
}
