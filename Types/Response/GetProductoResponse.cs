namespace PetraConectBack.Types.Response
{
    public class GetProductoResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public List<ProductoItemResponse> Productos { get; set; } = new List<ProductoItemResponse>();
    }
}
