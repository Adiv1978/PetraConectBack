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

    }
}
