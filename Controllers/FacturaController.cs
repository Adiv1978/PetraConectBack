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

        [HttpPost("GetFacturasByStatusActual")]
        public async Task<ActionResult<GetFacturaByStatusResponse>> GetFacturasByStatusActual([FromBody] GetFacturaByStatusRequest request)
        {
            try
            {
                MngFactura mngFactura = new MngFactura(_configuration);
                GetFacturaByStatusResponse response = await mngFactura.GetFacturasByStatusActual(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                GetFacturaByStatusResponse response = new GetFacturaByStatusResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    Facturas = new List<FacturaItemResponse>()
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                GetFacturaByStatusResponse response = new GetFacturaByStatusResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al consultar las facturas por estatus.",
                    Facturas = new List<FacturaItemResponse>()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpPost("SetFacturaStatus")]
        public async Task<ActionResult<SetFacturaStatusResponse>> SetFacturaStatus([FromBody] SetFacturaStatusRequest request)
        {
            try
            {
                MngFactura mngFactura = new MngFactura(_configuration);
                SetFacturaStatusResponse response = await mngFactura.SetFacturaStatus(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                SetFacturaStatusResponse response = new SetFacturaStatusResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    IdFactura = null,
                    Status = null
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                SetFacturaStatusResponse response = new SetFacturaStatusResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al registrar el estatus de la factura.",
                    IdFactura = null,
                    Status = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("SetFactura")]
        public async Task<ActionResult<SetFacturaResponse>> SetFactura([FromBody] SetFacturaRequest request)
        {
            try
            {
                MngFactura mngFactura = new MngFactura(_configuration);
                SetFacturaResponse response = await mngFactura.SetFactura(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                SetFacturaResponse response = new SetFacturaResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    FacturasAlegraConsultadas = 0,
                    FacturasExistentes = 0,
                    FacturasRegistradas = 0,
                    FacturasConError = 0,
                    Resultados = new List<SetFacturaItemResponse>()
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                SetFacturaResponse response = new SetFacturaResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al registrar facturas desde Alegra.",
                    FacturasAlegraConsultadas = 0,
                    FacturasExistentes = 0,
                    FacturasRegistradas = 0,
                    FacturasConError = 0,
                    Resultados = new List<SetFacturaItemResponse>()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
