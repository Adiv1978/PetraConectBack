namespace PetraConectBack.Types.Response
{
    public class SetFacturaResponse
    {
        public bool IsOk { get; set; }
        public string? Mensaje { get; set; }
        public int FacturasAlegraConsultadas { get; set; }
        public int FacturasExistentes { get; set; }
        public int FacturasRegistradas { get; set; }
        public int FacturasConError { get; set; }
        public List<SetFacturaItemResponse> Resultados { get; set; } = new();
    }
}
