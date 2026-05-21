using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Managers.L20
{
    public class MngFactura
    {
        private readonly L10.MngFactura _mngFacturaL10;
        private readonly L10.MngLog _mngLogL10;

        public MngFactura(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngFacturaL10 = new L10.MngFactura(configuration);
            _mngLogL10 = new L10.MngLog(configuration);
        }

        public async Task<GetFacturaResponse> GetFactura(GetFacturaRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngFactura.GetFactura - Entrada.");
                if (request == null)
                    throw new ClientException("La solicitud para consultar la factura no puede estar vacía.", "GET_FACTURA_REQUEST_NULL");
                if (string.IsNullOrWhiteSpace(request.SessionToken))
                    throw new ClientException("El token de sesión es obligatorio para consultar la factura.", "GET_FACTURA_SESSION_TOKEN_EMPTY");
                if (!request.IdFactura.HasValue && string.IsNullOrWhiteSpace(request.IdAlegra))
                    throw new ClientException("Debe enviar IdFactura o IdAlegra para consultar la factura.", "GET_FACTURA_FILTER_EMPTY");

                List<FacturaItemResponse>? facturas = await _mngFacturaL10.GetFactura(request);
                if (facturas == null)
                    throw new ClientException("No se recibió respuesta desde la base de datos al consultar la factura.", "GET_FACTURA_EMPTY_RESPONSE");

                GetFacturaResponse response = new GetFacturaResponse
                {
                    IsOk = true,
                    Mensaje = facturas.Count > 0 ? "Factura consultada correctamente" : "No se encontró la factura",
                    Facturas = facturas
                };

                _mngLogL10.WriteInfo("L20.MngFactura.GetFactura - Salida correcta. CantidadFacturas: " + facturas.Count);
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning("L20.MngFactura.GetFactura - Error controlado: " + ex.Message + " Código: " + ex.Codigo);
                throw;
            }
            catch (Exception ex)
            {
                _mngLogL10.WriteException(ex);
                throw new ClientException("Ocurrió un error interno al consultar la factura.", "GET_FACTURA_INTERNAL_ERROR", ex);
            }
        }
    }
}
