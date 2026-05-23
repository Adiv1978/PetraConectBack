using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;
using System.Linq;

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

                GetFacturaResponse response = new GetFacturaResponse { IsOk = true, Mensaje = facturas.Count > 0 ? "Factura consultada correctamente" : "No se encontró la factura", Facturas = facturas };
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

        public async Task<GetFacturaByStatusResponse> GetFacturasByStatusActual(GetFacturaByStatusRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngFactura.GetFacturasByStatusActual - Entrada.");
                if (request == null)
                    throw new ClientException("La solicitud para consultar facturas por estatus no puede estar vacía.", "GET_FACTURAS_STATUS_REQUEST_NULL");
                if (string.IsNullOrWhiteSpace(request.SessionToken))
                    throw new ClientException("El token de sesión es obligatorio para consultar facturas por estatus.", "GET_FACTURAS_STATUS_SESSION_TOKEN_EMPTY");
                if (string.IsNullOrWhiteSpace(request.Status))
                    throw new ClientException("El estatus es obligatorio para consultar facturas.", "GET_FACTURAS_STATUS_STATUS_EMPTY");

                string[] validStatuses = new[] { "Enviado", "Recibido", "Aceptado", "Procesando", "Entregado", "Rechazado", "Cancelado" };
                if (!validStatuses.Contains(request.Status, StringComparer.OrdinalIgnoreCase))
                    throw new ClientException("El estatus enviado no es válido.", "GET_FACTURAS_STATUS_INVALID");

                if (request.Limit.HasValue && (request.Limit.Value <= 0 || request.Limit.Value > 500))
                    throw new ClientException("El límite debe ser mayor a 0 y menor o igual a 500.", "GET_FACTURAS_STATUS_LIMIT_INVALID");

                List<FacturaItemResponse>? facturas = await _mngFacturaL10.GetFacturasByStatusActual(request);
                if (facturas == null)
                    throw new ClientException("No se recibió respuesta desde la base de datos al consultar facturas por estatus.", "GET_FACTURAS_STATUS_EMPTY_RESPONSE");

                GetFacturaByStatusResponse response = new GetFacturaByStatusResponse
                {
                    IsOk = true,
                    Mensaje = facturas.Count > 0 ? "Facturas consultadas correctamente" : "No se encontraron facturas para el estatus indicado",
                    Facturas = facturas
                };
                _mngLogL10.WriteInfo("L20.MngFactura.GetFacturasByStatusActual - Salida correcta. CantidadFacturas: " + facturas.Count);
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning("L20.MngFactura.GetFacturasByStatusActual - Error controlado: " + ex.Message + " Código: " + ex.Codigo);
                throw;
            }
            catch (Exception ex)
            {
                _mngLogL10.WriteException(ex);
                throw new ClientException("Ocurrió un error interno al consultar facturas por estatus.", "GET_FACTURAS_STATUS_INTERNAL_ERROR", ex);
            }
        }

        public async Task<SetFacturaDbResponse> SetFactura(SetFacturaDbRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngFactura.SetFactura - Entrada.");
                if (request == null)
                    throw new ClientException("La solicitud para registrar la factura no puede estar vacía.", "SET_FACTURA_REQUEST_NULL");
                if (string.IsNullOrWhiteSpace(request.SessionToken))
                    throw new ClientException("El token de sesión es obligatorio para registrar la factura.", "SET_FACTURA_SESSION_TOKEN_EMPTY");
                if (string.IsNullOrWhiteSpace(request.IdAlegra))
                    throw new ClientException("El Id de Alegra es obligatorio para registrar la factura.", "SET_FACTURA_ID_ALEGRA_EMPTY");
                if (request.ReferenciasProductos == null || request.ReferenciasProductos.Count == 0)
                    throw new ClientException("Debe enviar al menos una referencia de producto para registrar la factura.", "SET_FACTURA_REFERENCIAS_EMPTY");
                if (request.ReferenciasProductos.Any(x => string.IsNullOrWhiteSpace(x)))
                    throw new ClientException("No se permiten referencias de producto vacías.", "SET_FACTURA_REFERENCIA_ITEM_EMPTY");

                SetFacturaDbResponse? response = await _mngFacturaL10.SetFactura(request);
                if (response == null)
                    throw new ClientException("No se recibió respuesta desde la base de datos al registrar la factura.", "SET_FACTURA_DB_EMPTY_RESPONSE");
                if (!response.IsOk)
                    throw new ClientException(response.Mensaje ?? "No fue posible registrar la factura.", "SET_FACTURA_BUSINESS_ERROR");

                _mngLogL10.WriteInfo("L20.MngFactura.SetFactura - Salida correcta. IdFactura: " + response.IdFactura);
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning("L20.MngFactura.SetFactura - Error controlado: " + ex.Message + " Código: " + ex.Codigo);
                throw;
            }
            catch (Exception ex)
            {
                _mngLogL10.WriteException(ex);
                throw new ClientException("Ocurrió un error interno al registrar la factura.", "SET_FACTURA_INTERNAL_ERROR", ex);
            }
        }


        public async Task<SetFacturaStatusResponse> SetFacturaStatus(SetFacturaStatusRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngFactura.SetFacturaStatus - Entrada.");
                if (request == null)
                    throw new ClientException("La solicitud para registrar el estatus de la factura no puede estar vacía.", "SET_FACTURA_STATUS_REQUEST_NULL");
                if (string.IsNullOrWhiteSpace(request.SessionToken))
                    throw new ClientException("El token de sesión es obligatorio para registrar el estatus de la factura.", "SET_FACTURA_STATUS_SESSION_TOKEN_EMPTY");
                if (!request.IdFactura.HasValue)
                    throw new ClientException("El Id de la factura es obligatorio para registrar el estatus.", "SET_FACTURA_STATUS_ID_FACTURA_EMPTY");
                if (string.IsNullOrWhiteSpace(request.NuevoStatus))
                    throw new ClientException("El nuevo estatus de la factura es obligatorio.", "SET_FACTURA_STATUS_STATUS_EMPTY");

                string[] validStatuses = new[] { "Enviado", "Recibido", "Aceptado", "Procesando", "Entregado", "Rechazado", "Cancelado" };
                if (!validStatuses.Contains(request.NuevoStatus, StringComparer.OrdinalIgnoreCase))
                    throw new ClientException("El nuevo estatus enviado no es válido.", "SET_FACTURA_STATUS_STATUS_INVALID");

                SetFacturaStatusResponse? response = await _mngFacturaL10.SetFacturaStatus(request);
                if (response == null)
                    throw new ClientException("No se recibió respuesta desde la base de datos al registrar el estatus de la factura.", "SET_FACTURA_STATUS_DB_EMPTY_RESPONSE");
                if (!response.IsOk)
                    throw new ClientException(response.Mensaje ?? "No fue posible registrar el estatus de la factura.", "SET_FACTURA_STATUS_BUSINESS_ERROR");

                _mngLogL10.WriteInfo("L20.MngFactura.SetFacturaStatus - Salida correcta. IdFactura: " + response.IdFactura + " Status: " + response.Status);
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning("L20.MngFactura.SetFacturaStatus - Error controlado: " + ex.Message + " Código: " + ex.Codigo);
                throw;
            }
            catch (Exception ex)
            {
                _mngLogL10.WriteException(ex);
                throw new ClientException("Ocurrió un error interno al registrar el estatus de la factura.", "SET_FACTURA_STATUS_INTERNAL_ERROR", ex);
            }
        }

    }
}
