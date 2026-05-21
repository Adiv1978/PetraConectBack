using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;

namespace PetraConectBack.Managers.L30
{
    public class MngAlegra
    {
        private readonly L20.MngAlegra _mngAlegraL20;

        public MngAlegra(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngAlegraL20 = new L20.MngAlegra(configuration);
        }

        public async Task<GetLastFactResponse> GetLastFact(GetLastFactRequest request)
        {
            return await _mngAlegraL20.GetLastFact(request);
        }

        public async Task<GetItemsResponse> GetItems(GetItemsRequest request)
        {
            return await _mngAlegraL20.GetItems(request);
        }
    }
}
