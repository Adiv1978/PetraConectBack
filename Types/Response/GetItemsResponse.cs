using PetraConectBack.Types.External.Alegra;

namespace PetraConectBack.Types.Response
{
    public class GetItemsResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public int? Total { get; set; }
        public List<AlegraItemResponse> Items { get; set; } = new();
    }
}
