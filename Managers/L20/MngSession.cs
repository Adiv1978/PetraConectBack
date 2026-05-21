using Microsoft.Extensions.Configuration;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Managers.L20
{
    public class MngSession
    {
        private readonly L10.MngSession _mngSessionL10;
        private readonly L10.MngLog _mngLogL10;

        public MngSession(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngSessionL10 = new L10.MngSession(configuration);
            _mngLogL10 = new L10.MngLog(configuration);
        }

        public async Task<ValidateSessionResponse> ValidateSession(ValidateSessionRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngSession.ValidateSession - Entrada.");
                if (request == null)
                    throw new ClientException(
                        "La solicitud para validar la sesión no puede estar vacía.",
                        "VALIDATE_SESSION_REQUEST_NULL"
                    );

                ValidateSessionResponse? response = await _mngSessionL10.ValidateSession(request);
                if (response == null)
                    throw new ClientException(
                        "No se recibió respuesta desde la base de datos al validar la sesión.",
                        "VALIDATE_SESSION_DB_EMPTY_RESPONSE"
                    );

                if (!response.IsOk)
                    throw new ClientException(
                        response.Mensaje ?? "No fue posible validar la sesión.",
                        "VALIDATE_SESSION_BUSINESS_ERROR"
                    );

                _mngLogL10.WriteInfo(
                    "L20.MngSession.ValidateSession - Salida correcta. IdUsuario: " +
                    response.IdUsuario +
                    " IdSesion: " +
                    response.IdSesion
                );

                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning(
                    "L20.MngSession.ValidateSession - Error controlado: " +
                    ex.Message +
                    " Código: " +
                    ex.Codigo
                );
                throw;
            }
            catch (Exception ex)
            {
                _mngLogL10.WriteException(ex);
                throw new ClientException(
                    "Ocurrió un error interno al validar la sesión.",
                    "VALIDATE_SESSION_INTERNAL_ERROR",
                    ex
                );
            }
        }
    }
}
