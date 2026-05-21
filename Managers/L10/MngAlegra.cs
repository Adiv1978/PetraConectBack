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

            Dictionary<string, string?> queryParams = _alegraConverterL05.BuildGetItemsQueryParams(request, _itemsLimit);

            string json = await _alegraHelper.GetItemsAsync(request.EmailAlegra ?? string.Empty, request.KeyAlegra ?? string.Empty, queryParams);
            List<AlegraItemResponse> items = _alegraConverterL05.ConverterGetItems(json, out int? total);
            return (items, total);
        }
    }
}
