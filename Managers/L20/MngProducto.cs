using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Managers.L20
{
    public class MngProducto
    {
        private readonly L10.MngProducto _mngProductoL10;
        private readonly L10.MngLog _mngLogL10;

        public MngProducto(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngProductoL10 = new L10.MngProducto(configuration);
            _mngLogL10 = new L10.MngLog(configuration);
        }

        public async Task<SetProductoResponse> SetProducto(SetProductoRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngProducto.SetProducto - Entrada.");
                if (request == null)
                    throw new ClientException(
                        "La solicitud para registrar el producto no puede estar vacía.",
                        "SET_PRODUCTO_REQUEST_NULL"
                    );

                SetProductoResponse? response = await _mngProductoL10.SetProducto(request);
                if (response == null)
                    throw new ClientException(
                        "No se recibió respuesta desde la base de datos al registrar el producto.",
                        "SET_PRODUCTO_DB_EMPTY_RESPONSE"
                    );

                if (!response.IsOk)
                    throw new ClientException(
                        response.Mensaje ?? "No fue posible registrar el producto.",
                        "SET_PRODUCTO_BUSINESS_ERROR"
                    );

                _mngLogL10.WriteInfo("L20.MngProducto.SetProducto - Salida correcta. IdProducto: " + response.IdProducto);
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning("L20.MngProducto.SetProducto - Error controlado: " + ex.Message + " Código: " + ex.Codigo);
                throw;
            }
            catch (Exception ex)
            {
                _mngLogL10.WriteException(ex);
                throw new ClientException(
                    "Ocurrió un error interno al registrar el producto.",
                    "SET_PRODUCTO_INTERNAL_ERROR",
                    ex
                );
            }
        }
    }
}
