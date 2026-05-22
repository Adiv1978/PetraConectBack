namespace PetraConectBack.Types.Response
{
    public class SetFacturaDbResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public long? IdFactura { get; set; }
    }
}
