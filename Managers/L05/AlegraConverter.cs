using PetraConectBack.Types.External.Alegra;
using PetraConectBack.Types.Request;
using System.Text.Json;

namespace PetraConectBack.Managers.L05
{
    public class AlegraConverter
    {
        private readonly L04.AlegraConverter _alegraConverterL04;

        public AlegraConverter()
        {
            _alegraConverterL04 = new L04.AlegraConverter();
        }

        public Dictionary<string, string?> BuildGetItemsQueryParams(GetItemsRequest request, int defaultItemsLimit)
        {
            Dictionary<string, string?> queryParams = new Dictionary<string, string?>
            {
                ["limit"] = (request.Limit ?? defaultItemsLimit).ToString()
            };
            _alegraConverterL04.AddGetItemsOptionalQueryParams(request, queryParams);
            return queryParams;
        }

        public List<AlegraInvoiceResponse> ConverterGetLastFact(string json)
        {
            List<AlegraInvoiceResponse> list = new List<AlegraInvoiceResponse>();
            if (string.IsNullOrWhiteSpace(json))
                return list;

            using JsonDocument jsonDocument = JsonDocument.Parse(json);
            JsonElement root = jsonDocument.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement invoiceElement in root.EnumerateArray())
                    list.Add(_alegraConverterL04.ConverterGetLastFact(invoiceElement));
            }
            else if (root.ValueKind == JsonValueKind.Object
                && root.TryGetProperty("data", out JsonElement dataElement)
                && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement invoiceElement in dataElement.EnumerateArray())
                    list.Add(_alegraConverterL04.ConverterGetLastFact(invoiceElement));
            }

            return list;
        }

        public List<AlegraItemResponse> ConverterGetItems(string json, out int? total)
        {
            total = null;
            List<AlegraItemResponse> list = new List<AlegraItemResponse>();
            if (string.IsNullOrWhiteSpace(json))
                return list;

            using JsonDocument jsonDocument = JsonDocument.Parse(json);
            JsonElement root = jsonDocument.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement itemElement in root.EnumerateArray())
                    list.Add(_alegraConverterL04.ConverterGetItems(itemElement));
            }
            else if (root.ValueKind == JsonValueKind.Object)
            {
                if (root.TryGetProperty("metadata", out JsonElement metadataElement)
                    && metadataElement.ValueKind == JsonValueKind.Object
                    && metadataElement.TryGetProperty("total", out JsonElement totalElement)
                    && totalElement.TryGetInt32(out int totalValue))
                {
                    total = totalValue;
                }

                if (root.TryGetProperty("data", out JsonElement dataElement)
                    && dataElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement itemElement in dataElement.EnumerateArray())
                        list.Add(_alegraConverterL04.ConverterGetItems(itemElement));
                }
            }

            return list;
        }
    }
}
