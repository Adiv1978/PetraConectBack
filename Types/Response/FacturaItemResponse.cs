namespace PetraConectBack.Types.Response
{
    public class FacturaItemResponse
    {
        public long? IdFactura { get; set; }
        public string? IdAlegra { get; set; }
        public DateTime? RegFec { get; set; }
        public string? Observacion { get; set; }
        public bool IsOpen { get; set; }
        public string? StatusActual { get; set; }
        public DateTime? StatusFecReg { get; set; }
        public string? StatusComentario { get; set; }
        public long? IdUsuarioStatus { get; set; }
        public List<FacturaDetalleItemResponse> Detalle { get; set; } = new();
        public List<FacturaStatusItemResponse> Status { get; set; } = new();
        public string? DetalleRawJson { get; set; }
        public string? StatusRawJson { get; set; }
    }
}
