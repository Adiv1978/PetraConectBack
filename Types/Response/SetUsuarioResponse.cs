namespace PetraConectBack.Types.Response
{
    public class SetUsuarioResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public long? IdUsuario { get; set; }
    }
}
