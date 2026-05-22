namespace PetraConectBack.Types.Request
{
    public class SetFacturaDbRequest
    {
        public string? SessionToken { get; set; }
        public string? IdAlegra { get; set; }
        public string? Observacion { get; set; }
        public List<string> ReferenciasProductos { get; set; } = new();
    }
}
