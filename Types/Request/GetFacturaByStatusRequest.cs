namespace PetraConectBack.Types.Request
{
    public class GetFacturaByStatusRequest
    {
        public string? SessionToken { get; set; }
        public string? Status { get; set; }
        public int? Limit { get; set; }
    }
}
