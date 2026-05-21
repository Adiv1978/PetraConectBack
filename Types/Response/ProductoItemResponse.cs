namespace PetraConectBack.Types.Response
{
    public class ProductoItemResponse
    {
        public long Id { get; set; }
        public string? IdAlegra { get; set; }
        public string? Referencia { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool IsCocina { get; set; }
    }
}
