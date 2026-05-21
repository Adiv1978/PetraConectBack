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
    }
}
