namespace PetraConectBack.Types.Response
{
    public class SetFacturaStatusResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public long? IdFactura { get; set; }
        public string? Status { get; set; }
    }
}
