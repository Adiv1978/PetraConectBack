using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;

namespace PetraConectBack.Managers.L20
{
    public class MngAlegra
    {
        private readonly L10.MngAlegra _mngAlegraL10;
        private readonly L10.MngLog _mngLogL10;

        public MngAlegra(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngAlegraL10 = new L10.MngAlegra(configuration);
            _mngLogL10 = new L10.MngLog(configuration);
        }

        public async Task<GetLastFactResponse> GetLastFact(GetLastFactRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngAlegra.GetLastFact - Entrada.");
                if (request == null)
                    throw new ClientException("La solicitud para consultar facturas de Alegra no puede estar vacía.", "GET_LAST_FACT_REQUEST_NULL");
                if (string.IsNullOrWhiteSpace(request.EmailAlegra))
                    throw new ClientException("El correo de Alegra es obligatorio para consultar facturas.", "GET_LAST_FACT_EMAIL_ALEGRA_EMPTY");
                if (string.IsNullOrWhiteSpace(request.KeyAlegra))
                    throw new ClientException("La llave de Alegra es obligatoria para consultar facturas.", "GET_LAST_FACT_KEY_ALEGRA_EMPTY");

                var facturas = await _mngAlegraL10.GetLastFact(request);
                if (facturas == null)
                    throw new ClientException("No se recibió respuesta de facturas desde Alegra.", "GET_LAST_FACT_EMPTY_RESPONSE");

                GetLastFactResponse response = new GetLastFactResponse
                {
                    IsOk = true,
                    Mensaje = facturas.Count > 0 ? "Facturas consultadas correctamente" : "No se encontraron facturas registradas en la fecha actual",
                    Facturas = facturas
                };

                _mngLogL10.WriteInfo("L20.MngAlegra.GetLastFact - Salida correcta. CantidadFacturas: " + facturas.Count);
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning("L20.MngAlegra.GetLastFact - Error controlado: " + ex.Message + " Código: " + ex.Codigo);
                throw;
            }
            catch (Exception ex)
            {
                _mngLogL10.WriteException(ex);
                throw new ClientException("Ocurrió un error interno al consultar las facturas de Alegra.", "GET_LAST_FACT_INTERNAL_ERROR", ex);
            }
        }

        public async Task<GetItemsResponse> GetItems(GetItemsRequest request)
        {
            try
            {
                _mngLogL10.WriteInfo("L20.MngAlegra.GetItems - Entrada.");
                if (request == null)
                    throw new ClientException("La solicitud para consultar items de Alegra no puede estar vacía.", "GET_ITEMS_REQUEST_NULL");
                if (string.IsNullOrWhiteSpace(request.EmailAlegra))
                    throw new ClientException("El correo de Alegra es obligatorio para consultar items.", "GET_ITEMS_EMAIL_ALEGRA_EMPTY");
                if (string.IsNullOrWhiteSpace(request.KeyAlegra))
                    throw new ClientException("La llave de Alegra es obligatoria para consultar items.", "GET_ITEMS_KEY_ALEGRA_EMPTY");
                if (request.Limit.HasValue && (request.Limit.Value > 30 || request.Limit.Value <= 0))
                    throw new ClientException("El límite de items debe ser mayor que cero y no mayor que 30.", "GET_ITEMS_LIMIT_INVALID");
                if (request.Start.HasValue && request.Start.Value < 0)
                    throw new ClientException("El parámetro start no puede ser menor que cero.", "GET_ITEMS_START_INVALID");
                if (!string.IsNullOrWhiteSpace(request.OrderDirection) && request.OrderDirection != "ASC" && request.OrderDirection != "DESC")
                    throw new ClientException("El parámetro orderDirection solo permite ASC o DESC.", "GET_ITEMS_ORDER_DIRECTION_INVALID");
                if (!string.IsNullOrWhiteSpace(request.OrderField) && request.OrderField != "name" && request.OrderField != "id" && request.OrderField != "reference" && request.OrderField != "description")
                    throw new ClientException("El parámetro orderField solo permite name, id, reference o description.", "GET_ITEMS_ORDER_FIELD_INVALID");
                if (!string.IsNullOrWhiteSpace(request.Status) && request.Status != "active" && request.Status != "inactive")
                    throw new ClientException("El parámetro status solo permite active o inactive.", "GET_ITEMS_STATUS_INVALID");
                if (!string.IsNullOrWhiteSpace(request.Mode) && request.Mode != "simple" && request.Mode != "advanced")
                    throw new ClientException("El parámetro mode solo permite simple o advanced.", "GET_ITEMS_MODE_INVALID");

                var result = await _mngAlegraL10.GetItems(request);
                if (result.Items == null)
                    throw new ClientException("No se recibió respuesta de items desde Alegra.", "GET_ITEMS_EMPTY_RESPONSE");

                GetItemsResponse response = new GetItemsResponse
                {
                    IsOk = true,
                    Mensaje = result.Items.Count > 0 ? "Items consultados correctamente" : "No se encontraron items",
                    Total = result.Total,
                    Items = result.Items
                };

                _mngLogL10.WriteInfo("L20.MngAlegra.GetItems - Salida correcta. CantidadItems: " + result.Items.Count);
                return response;
            }
            catch (ClientException ex)
            {
                _mngLogL10.WriteWarning("L20.MngAlegra.GetItems - Error controlado: " + ex.Message + " Código: " + ex.Codigo);
                throw;
            }
            catch (Exception ex)
            {
                _mngLogL10.WriteException(ex);
                throw new ClientException("Ocurrió un error interno al consultar los items de Alegra.", "GET_ITEMS_INTERNAL_ERROR", ex);
            }
        }
    }
}
