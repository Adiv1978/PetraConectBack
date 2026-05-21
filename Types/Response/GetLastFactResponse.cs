using PetraConectBack.Types.External.Alegra;

namespace PetraConectBack.Types.Response
{
    public class GetLastFactResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public List<AlegraInvoiceResponse> Facturas { get; set; } = new();
    }
}
