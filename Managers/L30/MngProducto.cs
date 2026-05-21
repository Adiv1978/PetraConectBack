using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;

namespace PetraConectBack.Managers.L30
{
    public class MngProducto
    {
        private readonly L20.MngProducto _mngProductoL20;

        public MngProducto(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngProductoL20 = new L20.MngProducto(configuration);
        }

        public async Task<SetProductoResponse> SetProducto(SetProductoRequest request)
        {
            return await _mngProductoL20.SetProducto(request);
        }

        public async Task<UpdateProductoResponse> UpdateProducto(UpdateProductoRequest request)
        {
            return await _mngProductoL20.UpdateProducto(request);
        }
    }
}
