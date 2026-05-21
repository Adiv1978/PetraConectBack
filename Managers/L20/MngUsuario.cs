using Microsoft.Extensions.Configuration;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Managers.L20
{
    public class MngUsuario
    {
        private readonly L10.MngUsuario _mngUsuarioL10;
        private readonly L10.MngLog _mngLogL10;

        public MngUsuario(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngUsuarioL10 = new L10.MngUsuario(configuration);
            _mngLogL10 = new L10.MngLog(configuration);
        }

        public async Task<SetUsuarioResponse> SetUsuario(SetUsuarioRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngUsuario.SetUsuario - Entrada.");
                if (request == null)
                    throw new ClientException(
                        "La solicitud para registrar el usuario no puede estar vacía.",
                        "SET_USUARIO_REQUEST_NULL"
                    );
                SetUsuarioResponse? response =  await _mngUsuarioL10.SetUsuario(request);
                if (response == null)
                    throw new ClientException(
                        "No se recibió respuesta desde la base de datos al registrar el usuario.",
                        "SET_USUARIO_DB_EMPTY_RESPONSE"
                    );
                if (!response.IsOk)
                    throw new ClientException(
                        response.Mensaje ?? "No fue posible registrar el usuario.",
                        "SET_USUARIO_BUSINESS_ERROR"
                    );
                _mngLogL10.WriteInfo(
                    "L20.MngUsuario.SetUsuario - Salida correcta. IdUsuario: " +
                    response.IdUsuario
                );
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning(
                    "L20.MngUsuario.SetUsuario - Error controlado: " +
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
                    "Ocurrió un error interno al registrar el usuario.",
                    "SET_USUARIO_INTERNAL_ERROR",
                    ex
                );
            }
        }

        public async Task<UpdateUsuarioResponse> UpdateUser(UpdateUsuarioRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngUsuario.UpdateUser - Entrada.");
                if (request == null)
                    throw new ClientException(
                        "La solicitud para actualizar el usuario no puede estar vacía.",
                        "UPDATE_USUARIO_REQUEST_NULL"
                    );
                UpdateUsuarioResponse? response =
                    await _mngUsuarioL10.UpdateUser(request);
                if (response == null)
                    throw new ClientException(
                        "No se recibió respuesta desde la base de datos al actualizar el usuario.",
                        "UPDATE_USUARIO_DB_EMPTY_RESPONSE"
                    );
                if (!response.IsOk)
                    throw new ClientException(
                        response.Mensaje ?? "No fue posible actualizar el usuario.",
                        "UPDATE_USUARIO_BUSINESS_ERROR"
                    );
                _mngLogL10.WriteInfo(
                    "L20.MngUsuario.UpdateUser - Salida correcta. IdUsuario: " +
                    response.IdUsuario
                );
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning(
                    "L20.MngUsuario.UpdateUser - Error controlado: " +
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
                    "Ocurrió un error interno al actualizar el usuario.",
                    "UPDATE_USUARIO_INTERNAL_ERROR",
                    ex
                );
            }
        }

        public async Task<LoginUsuarioResponse> LoginUsuario(LoginUsuarioRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngUsuario.LoginUsuario - Entrada.");
                if (request == null)
                    throw new ClientException(
                        "La solicitud para iniciar sesión no puede estar vacía.",
                        "LOGIN_USUARIO_REQUEST_NULL"
                    );

                LoginUsuarioResponse? response = await _mngUsuarioL10.LoginUsuario(request);
                if (response == null)
                    throw new ClientException(
                        "No se recibió respuesta desde la base de datos al iniciar sesión.",
                        "LOGIN_USUARIO_DB_EMPTY_RESPONSE"
                    );

                if (!response.IsOk)
                    throw new ClientException(
                        response.Mensaje ?? "No fue posible iniciar sesión.",
                        "LOGIN_USUARIO_BUSINESS_ERROR"
                    );

                _mngLogL10.WriteInfo(
                    "L20.MngUsuario.LoginUsuario - Salida correcta. IdUsuario: " +
                    response.IdUsuario +
                    " IdSesion: " +
                    response.IdSesion
                );

                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning(
                    "L20.MngUsuario.LoginUsuario - Error controlado: " +
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
                    "Ocurrió un error interno al iniciar sesión.",
                    "LOGIN_USUARIO_INTERNAL_ERROR",
                    ex
                );
            }
        }

        public async Task<GetUsuarioBySessionTokenResponse> GetUsuarioBySessionToken(GetUsuarioBySessionTokenRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngUsuario.GetUsuarioBySessionToken - Entrada.");
                if (request == null)
                    throw new ClientException(
                        "La solicitud para consultar el usuario por token de sesión no puede estar vacía.",
                        "GET_USUARIO_SESSION_TOKEN_REQUEST_NULL"
                    );
                if (string.IsNullOrWhiteSpace(request.SessionToken))
                    throw new ClientException(
                        "El token de sesión no puede estar vacío.",
                        "GET_USUARIO_SESSION_TOKEN_EMPTY"
                    );

                GetUsuarioBySessionTokenResponse? response = await _mngUsuarioL10.GetUsuarioBySessionToken(request);
                if (response == null)
                    throw new ClientException(
                        "No se recibió respuesta desde la base de datos al consultar el usuario por token de sesión.",
                        "GET_USUARIO_SESSION_TOKEN_DB_EMPTY_RESPONSE"
                    );

                if (!response.IsOk)
                    throw new ClientException(
                        response.Mensaje ?? "No fue posible consultar el usuario por token de sesión.",
                        "GET_USUARIO_SESSION_TOKEN_BUSINESS_ERROR"
                    );

                _mngLogL10.WriteInfo(
                    "L20.MngUsuario.GetUsuarioBySessionToken - Salida correcta. IdUsuario: " +
                    response.IdUsuario +
                    " Nick: " +
                    response.Nick
                );

                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning(
                    "L20.MngUsuario.GetUsuarioBySessionToken - Error controlado: " +
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
                    "Ocurrió un error interno al consultar el usuario por token de sesión.",
                    "GET_USUARIO_SESSION_TOKEN_INTERNAL_ERROR",
                    ex
                );
            }
        }

    }
}
