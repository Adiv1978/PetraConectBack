namespace PetraConectBack.Types.Response
{
    public class SetProductoResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public long? IdProducto { get; set; }
    }
}
