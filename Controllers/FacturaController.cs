using Microsoft.AspNetCore.Mvc;
using PetraConectBack.Managers.L30;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public FacturaController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("GetFactura")]
        public async Task<ActionResult<GetFacturaResponse>> GetFactura([FromBody] GetFacturaRequest request)
        {
            try
            {
                MngFactura mngFactura = new MngFactura(_configuration);
                GetFacturaResponse response = await mngFactura.GetFactura(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                GetFacturaResponse response = new GetFacturaResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    Facturas = new List<FacturaItemResponse>()
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                GetFacturaResponse response = new GetFacturaResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al consultar la factura.",
                    Facturas = new List<FacturaItemResponse>()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
