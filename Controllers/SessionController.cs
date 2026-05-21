using Microsoft.AspNetCore.Mvc;
using PetraConectBack.Managers.L30;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SessionController(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("ValidateSession")]
        public async Task<ActionResult<ValidateSessionResponse>> ValidateSession([FromBody] ValidateSessionRequest request)
        {
            try
            {
                MngSession mngSession = new MngSession(_configuration);
                ValidateSessionResponse response = await mngSession.ValidateSession(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                ValidateSessionResponse response = new ValidateSessionResponse
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
                ValidateSessionResponse response = new ValidateSessionResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al validar la sesión.",
                    IdUsuario = null,
                    IdSesion = null,
                    SessionToken = null,
                    FecCaduca = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
