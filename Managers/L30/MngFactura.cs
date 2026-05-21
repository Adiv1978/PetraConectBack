using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;

namespace PetraConectBack.Managers.L30
{
    public class MngFactura
    {
        private readonly L20.MngFactura _mngFacturaL20;

        public MngFactura(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngFacturaL20 = new L20.MngFactura(configuration);
        }

        public async Task<GetFacturaResponse> GetFactura(GetFacturaRequest request)
        {
            return await _mngFacturaL20.GetFactura(request);
        }

        public async Task<GetFacturaByStatusResponse> GetFacturasByStatusActual(GetFacturaByStatusRequest request)
        {
            return await _mngFacturaL20.GetFacturasByStatusActual(request);
        }
    }
}
