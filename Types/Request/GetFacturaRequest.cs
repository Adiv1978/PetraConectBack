namespace PetraConectBack.Types.Request
{
    public class GetFacturaRequest
    {
        public string? SessionToken { get; set; }
        public long? IdFactura { get; set; }
        public string? IdAlegra { get; set; }
    }
}
