namespace PetraConectBack.Types.Response
{
    public class GetUsuarioBySessionTokenResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public long? IdUsuario { get; set; }
        public string? Nick { get; set; }
        public string? EmailAlegra { get; set; }
        public string? KeyAlegra { get; set; }
    }
}
