using Microsoft.AspNetCore.Mvc;
using PetraConectBack.Managers.L30;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("SetUsuario")]
        public async Task<ActionResult<SetUsuarioResponse>> SetUsuario(
            [FromBody] SetUsuarioRequest request)
        {
            try
            {
                if (request != null &&
                    string.IsNullOrWhiteSpace(request.SessionToken) &&
                    Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                {
                    string sessionToken = authorizationHeader.ToString();
                    const string bearerPrefix = "Bearer ";
                    if (sessionToken.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
                        sessionToken = sessionToken.Substring(bearerPrefix.Length).Trim();

                    if (!string.IsNullOrWhiteSpace(sessionToken))
                        request.SessionToken = sessionToken;
                }

                MngUsuario mngUsuario = new MngUsuario(_configuration);
                SetUsuarioResponse response =
                    await mngUsuario.SetUsuario(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                SetUsuarioResponse response = new SetUsuarioResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    IdUsuario = null
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                SetUsuarioResponse response = new SetUsuarioResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al registrar el usuario.",
                    IdUsuario = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<UpdateUsuarioResponse>> UpdateUser(
            [FromBody] UpdateUsuarioRequest request)
        {
            try
            {
                MngUsuario mngUsuario = new MngUsuario(_configuration);
                UpdateUsuarioResponse response =
                    await mngUsuario.UpdateUser(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                UpdateUsuarioResponse response = new UpdateUsuarioResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    IdUsuario = null
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                UpdateUsuarioResponse response = new UpdateUsuarioResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al actualizar el usuario.",
                    IdUsuario = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("LoginUsuario")]
        public async Task<ActionResult<LoginUsuarioResponse>> LoginUsuario([FromBody] LoginUsuarioRequest request)
        {
            try
            {
                MngUsuario mngUsuario = new MngUsuario(_configuration);
                LoginUsuarioResponse response = await mngUsuario.LoginUsuario(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                LoginUsuarioResponse response = new LoginUsuarioResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    IdUsuario = null,
                    IdSesion = null,
                    SessionToken = null,
                    FecCaduca = null
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                LoginUsuarioResponse response = new LoginUsuarioResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al iniciar sesión.",
                    IdUsuario = null,
                    IdSesion = null,
                    SessionToken = null,
                    FecCaduca = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("GetUsuarioBySessionToken")]
        public async Task<ActionResult<GetUsuarioBySessionTokenResponse>> GetUsuarioBySessionToken([FromBody] GetUsuarioBySessionTokenRequest request)
        {
            try
            {
                MngUsuario mngUsuario = new MngUsuario(_configuration);
                GetUsuarioBySessionTokenResponse response =
                    await mngUsuario.GetUsuarioBySessionToken(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                GetUsuarioBySessionTokenResponse response = new GetUsuarioBySessionTokenResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    IdUsuario = null,
                    Nick = null,
                    EmailAlegra = null,
                    KeyAlegra = null
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                GetUsuarioBySessionTokenResponse response = new GetUsuarioBySessionTokenResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al consultar el usuario por token de sesión.",
                    IdUsuario = null,
                    Nick = null,
                    EmailAlegra = null,
                    KeyAlegra = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
