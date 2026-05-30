namespace PetraConectBack.Types.Request
{
    public class SetUsuarioRequest
    {
        public string? SessionToken { get; set; }
        public string? Nick { get; set; }
        public string? Pass { get; set; }
        public string? EmailAlegra { get; set; }
        public string? KeyAlegra { get; set; }
    }
}
