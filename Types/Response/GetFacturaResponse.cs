namespace PetraConectBack.Types.Response
{
    public class GetFacturaResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public List<FacturaItemResponse> Facturas { get; set; } = new();
    }
}
