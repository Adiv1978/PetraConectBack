namespace PetraConectBack.Types.Request
{
    public class UpdateProductoRequest
    {
        public string? SessionToken { get; set; }
        public long? IdProducto { get; set; }
        public string? IdAlegra { get; set; }
        public string? Referencia { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool IsCocina { get; set; }
    }
}
