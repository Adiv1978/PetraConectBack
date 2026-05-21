using Microsoft.AspNetCore.Mvc;
using PetraConectBack.Managers.L30;
using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("SetProducto")]
        public async Task<ActionResult<SetProductoResponse>> SetProducto([FromBody] SetProductoRequest request)
        {
            try
            {
                MngProducto mngProducto = new MngProducto(_configuration);
                SetProductoResponse response = await mngProducto.SetProducto(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                SetProductoResponse response = new SetProductoResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    IdProducto = null
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                SetProductoResponse response = new SetProductoResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al registrar el producto.",
                    IdProducto = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("UpdateProducto")]
        public async Task<ActionResult<UpdateProductoResponse>> UpdateProducto([FromBody] UpdateProductoRequest request)
        {
            try
            {
                MngProducto mngProducto = new MngProducto(_configuration);
                UpdateProductoResponse response = await mngProducto.UpdateProducto(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                UpdateProductoResponse response = new UpdateProductoResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    IdProducto = null
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                UpdateProductoResponse response = new UpdateProductoResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al actualizar el producto.",
                    IdProducto = null
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }


        [HttpPost("GetProducto")]
        public async Task<ActionResult<GetProductoResponse>> GetProducto([FromBody] GetProductoRequest request)
        {
            try
            {
                MngProducto mngProducto = new MngProducto(_configuration);
                GetProductoResponse response = await mngProducto.GetProducto(request);
                return Ok(response);
            }
            catch (ClientException ex)
            {
                GetProductoResponse response = new GetProductoResponse
                {
                    IsOk = false,
                    Mensaje = ex.Message,
                    Productos = new List<ProductoItemResponse>()
                };
                return BadRequest(response);
            }
            catch (Exception)
            {
                GetProductoResponse response = new GetProductoResponse
                {
                    IsOk = false,
                    Mensaje = "Ocurrió un error interno en el servidor al consultar productos.",
                    Productos = new List<ProductoItemResponse>()
                };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
