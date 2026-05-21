namespace PetraConectBack.Types.Request
{
    public class GetItemsRequest
    {
        public string? EmailAlegra { get; set; }
        public string? KeyAlegra { get; set; }
        public int? Start { get; set; }
        public int? Limit { get; set; }
        public string? OrderDirection { get; set; }
        public string? OrderField { get; set; }
        public string? Query { get; set; }
        public bool? Metadata { get; set; }
        public string? IdWarehouse { get; set; }
        public string? Name { get; set; }
        public string? Reference { get; set; }
        public string? Description { get; set; }
        public string? PriceListId { get; set; }
        public string? IdItemCategory { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public bool? Inventariable { get; set; }
        public string? Fields { get; set; }
        public string? Mode { get; set; }
    }
}
