namespace PetraConectBack.Types.Response
{
    public class SetFacturaItemResponse
    {
        public string? IdAlegra { get; set; }
        public bool ExisteLocalmente { get; set; }
        public bool Registrada { get; set; }
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public long? IdFacturaLocal { get; set; }
        public List<string> ReferenciasProductos { get; set; } = new();
    }
}
