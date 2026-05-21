using Microsoft.Extensions.Configuration;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;

namespace PetraConectBack.Managers.L30
{
    public class MngUsuario
    {
        private readonly L20.MngUsuario _mngUsuarioL20;

        public MngUsuario(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngUsuarioL20 = new L20.MngUsuario(configuration);
        }

        public async Task<SetUsuarioResponse> SetUsuario(SetUsuarioRequest request)
        {
            return await _mngUsuarioL20.SetUsuario(request);
        }

        public async Task<UpdateUsuarioResponse> UpdateUser(UpdateUsuarioRequest request)
        {
            return await _mngUsuarioL20.UpdateUser(request);
        }
    }
}
