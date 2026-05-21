namespace PetraConectBack.Types.Response
{
    public class FacturaStatusItemResponse
    {
        public long? IdFacturaStatus { get; set; }
        public long? IdUsuario { get; set; }
        public string? UsuarioNick { get; set; }
        public DateTime? FecReg { get; set; }
        public string? Comentario { get; set; }
        public string? Status { get; set; }
        public string? RawJson { get; set; }
    }
}
