namespace PetraConectBack.Types.Request
{
    public class SetFacturaStatusRequest
    {
        public string? SessionToken { get; set; }
        public long? IdFactura { get; set; }
        public string? NuevoStatus { get; set; }
        public string? Comentario { get; set; }
    }
}
