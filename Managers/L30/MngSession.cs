using Microsoft.Extensions.Configuration;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;

namespace PetraConectBack.Managers.L30
{
    public class MngSession
    {
        private readonly L20.MngSession _mngSessionL20;

        public MngSession(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngSessionL20 = new L20.MngSession(configuration);
        }

        public async Task<ValidateSessionResponse> ValidateSession(ValidateSessionRequest request)
        {
            return await _mngSessionL20.ValidateSession(request);
        }
    }
}
