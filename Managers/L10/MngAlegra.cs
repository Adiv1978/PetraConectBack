using PetraConectBack.Managers.L00;
using PetraConectBack.Types.External.Alegra;
using PetraConectBack.Types.Request;

namespace PetraConectBack.Managers.L10
{
    public class MngAlegra
    {
        private readonly AlegraHelper _alegraHelper;
        private readonly L05.AlegraConverter _alegraConverterL05;
        private readonly int _lastFactLimit;
        private readonly int _itemsLimit;

        public MngAlegra(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            SettingHelper settingHelper = new SettingHelper(configuration);
            string itemsUrl = settingHelper.GetAlegraItemsUrl();
            string invoicesUrl = settingHelper.GetAlegraInvoicesUrl();
            _lastFactLimit = settingHelper.GetAlegraLastFactLimit();
            _itemsLimit = settingHelper.GetAlegraItemsLimit();
            _alegraHelper = new AlegraHelper(itemsUrl, invoicesUrl);
            _alegraConverterL05 = new L05.AlegraConverter();
        }

        public async Task<List<AlegraInvoiceResponse>> GetLastFact(GetLastFactRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            Dictionary<string, string?> queryParams = new Dictionary<string, string?>
            {
                ["limit"] = _lastFactLimit.ToString(),
                ["order_direction"] = "DESC",
                ["order_field"] = "id",
                ["date"] = DateTime.Now.ToString("yyyy-MM-dd")
            };
            string json = await _alegraHelper.GetInvoicesAsync(request.EmailAlegra ?? string.Empty, request.KeyAlegra ?? string.Empty, queryParams);
            return _alegraConverterL05.ConverterGetLastFact(json);
        }

        public async Task<(List<AlegraItemResponse> Items, int? Total)> GetItems(GetItemsRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            Dictionary<string, string?> queryParams = new Dictionary<string, string?>
            {
                ["limit"] = (request.Limit ?? _itemsLimit).ToString()
            };

            if (request.Start.HasValue) queryParams["start"] = request.Start.Value.ToString();
            if (!string.IsNullOrWhiteSpace(request.OrderDirection)) queryParams["order_direction"] = request.OrderDirection;
            if (!string.IsNullOrWhiteSpace(request.OrderField)) queryParams["order_field"] = request.OrderField;
            if (!string.IsNullOrWhiteSpace(request.Query)) queryParams["query"] = request.Query;
            if (request.Metadata.HasValue) queryParams["metadata"] = request.Metadata.Value.ToString().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(request.IdWarehouse)) queryParams["idWarehouse"] = request.IdWarehouse;
            if (!string.IsNullOrWhiteSpace(request.Name)) queryParams["name"] = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Reference)) queryParams["reference"] = request.Reference;
            if (!string.IsNullOrWhiteSpace(request.Description)) queryParams["description"] = request.Description;
            if (!string.IsNullOrWhiteSpace(request.PriceListId)) queryParams["priceList_id"] = request.PriceListId;
            if (!string.IsNullOrWhiteSpace(request.IdItemCategory)) queryParams["idItemCategory"] = request.IdItemCategory;
            if (!string.IsNullOrWhiteSpace(request.Type)) queryParams["type"] = request.Type;
            if (!string.IsNullOrWhiteSpace(request.Status)) queryParams["status"] = request.Status;
            if (request.Inventariable.HasValue) queryParams["inventariable"] = request.Inventariable.Value.ToString().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(request.Fields)) queryParams["fields"] = request.Fields;
            if (!string.IsNullOrWhiteSpace(request.Mode)) queryParams["mode"] = request.Mode;

            string json = await _alegraHelper.GetItemsAsync(request.EmailAlegra ?? string.Empty, request.KeyAlegra ?? string.Empty, queryParams);
            List<AlegraItemResponse> items = _alegraConverterL05.ConverterGetItems(json, out int? total);
            return (items, total);
        }
    }
}
