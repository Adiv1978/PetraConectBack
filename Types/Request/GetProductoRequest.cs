namespace PetraConectBack.Types.Request
{
    public class GetProductoRequest
    {
        public string? SessionToken { get; set; }
        public long? IdProducto { get; set; }
        public string? IdAlegra { get; set; }
        public string? Referencia { get; set; }
        public string? Nombre { get; set; }
        public int? Limit { get; set; }
    }
}
