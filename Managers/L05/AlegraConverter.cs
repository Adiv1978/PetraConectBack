using PetraConectBack.Types.External.Alegra;
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
    }
}
