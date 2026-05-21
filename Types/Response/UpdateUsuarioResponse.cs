namespace PetraConectBack.Types.Response
{
    public class UpdateUsuarioResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public long? IdUsuario { get; set; }
    }
}
