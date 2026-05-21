namespace PetraConectBack.Types.External.Alegra
{
    public class AlegraInvoiceItemResponse
    {
        public string? Id { get; set; }
        public string? IdItem { get; set; }
        public string? Name { get; set; }
        public string? Reference { get; set; }
        public string? Description { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public string? RawJson { get; set; }
    }
}
