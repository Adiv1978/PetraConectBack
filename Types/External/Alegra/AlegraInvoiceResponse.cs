namespace PetraConectBack.Types.External.Alegra
{
    public class AlegraInvoiceResponse
    {
        public string? Id { get; set; }
        public string? Date { get; set; }
        public string? DueDate { get; set; }
        public string? Datetime { get; set; }
        public string? Observations { get; set; }
        public string? Anotation { get; set; }
        public string? TermsConditions { get; set; }
        public string? Status { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? Balance { get; set; }
        public string? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientIdentification { get; set; }
        public string? ClientEmail { get; set; }
        public string? NumberTemplateId { get; set; }
        public string? NumberTemplatePrefix { get; set; }
        public string? NumberTemplateNumber { get; set; }
        public List<AlegraInvoiceItemResponse> Items { get; set; } = new();
        public string? RawJson { get; set; }
    }
}
