using Microsoft.AspNetCore.Mvc;
using PetraConectBack.Managers.L30;
using PetraConectBack.Types.External.Alegra;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlegraController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AlegraController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("GetLastFact")]
        public async Task<ActionResult<GetLastFactResponse>> GetLastFact([FromBody] GetLastFactRequest request)
        {
            try
            {
                MngAlegra mngAlegra = new MngAlegra(_configuration);
                GetLastFactResponse response = await mngAlegra.GetLastFact(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                return BadRequest(new GetLastFactResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    Facturas = new List<AlegraInvoiceResponse>()
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GetLastFactResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al consultar las facturas de Alegra.",
                    Facturas = new List<AlegraInvoiceResponse>()
                });
            }
        }

        [HttpPost("GetItems")]
        public async Task<ActionResult<GetItemsResponse>> GetItems([FromBody] GetItemsRequest request)
        {
            try
            {
                MngAlegra mngAlegra = new MngAlegra(_configuration);
                GetItemsResponse response = await mngAlegra.GetItems(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                return BadRequest(new GetItemsResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    Total = null,
                    Items = new List<AlegraItemResponse>()
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new GetItemsResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al consultar los items de Alegra.",
                    Total = null,
                    Items = new List<AlegraItemResponse>()
                });
            }
        }
    }
}
