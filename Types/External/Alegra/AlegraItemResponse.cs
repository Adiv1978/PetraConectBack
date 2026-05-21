namespace PetraConectBack.Types.External.Alegra
{
    public class AlegraItemResponse
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Reference { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? Inventariable { get; set; }
        public string? InventoryUnit { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? InitialQuantity { get; set; }
        public List<AlegraItemPriceResponse> Prices { get; set; } = new();
        public string? RawJson { get; set; }
    }
}
