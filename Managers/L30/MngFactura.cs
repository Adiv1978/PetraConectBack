using PetraConectBack.Types.Request;
using PetraConectBack.Types.Response;
using PetraConectBack.Types.Utility;
using System.Linq;

namespace PetraConectBack.Managers.L30
{
    public class MngFactura
    {
        private readonly L20.MngFactura _mngFacturaL20;
        private readonly L20.MngUsuario _mngUsuarioL20;
        private readonly L20.MngAlegra _mngAlegraL20;

        public MngFactura(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _mngFacturaL20 = new L20.MngFactura(configuration);
            _mngUsuarioL20 = new L20.MngUsuario(configuration);
            _mngAlegraL20 = new L20.MngAlegra(configuration);
        }

        public async Task<GetFacturaResponse> GetFactura(GetFacturaRequest request)
        {
            return await _mngFacturaL20.GetFactura(request);
        }

        public async Task<GetFacturaByStatusResponse> GetFacturasByStatusActual(GetFacturaByStatusRequest request)
        {
            return await _mngFacturaL20.GetFacturasByStatusActual(request);
        }

        public async Task<SetFacturaResponse> SetFactura(SetFacturaRequest request)
        {
            if (request == null)
                throw new ClientException("La solicitud para sincronizar facturas no puede estar vacía.", "SET_FACTURA_REQUEST_NULL");
            if (string.IsNullOrWhiteSpace(request.SessionToken))
                throw new ClientException("El token de sesión es obligatorio para sincronizar facturas.", "SET_FACTURA_SESSION_TOKEN_EMPTY");

            GetUsuarioBySessionTokenResponse usuario = await _mngUsuarioL20.GetUsuarioBySessionToken(new GetUsuarioBySessionTokenRequest { SessionToken = request.SessionToken });
            if (usuario == null || !usuario.IsOk)
                throw new ClientException(usuario?.Mensaje ?? "No fue posible obtener el usuario por token de sesión.", "SET_FACTURA_USER_ERROR");

            GetLastFactResponse facturasAlegra = await _mngAlegraL20.GetLastFact(new GetLastFactRequest
            {
                EmailAlegra = usuario.EmailAlegra,
                KeyAlegra = usuario.KeyAlegra
            });

            List<SetFacturaItemResponse> resultados = new List<SetFacturaItemResponse>();
            int facturasExistentes = 0;
            int facturasRegistradas = 0;
            int facturasConError = 0;
            List<Types.External.Alegra.AlegraInvoiceResponse> facturas = facturasAlegra.Facturas ?? new List<Types.External.Alegra.AlegraInvoiceResponse>();

            foreach (var factura in facturas)
            {
                string? idAlegra = factura.Id;
                List<string> referencias = factura.Items
                    .Where(x => !string.IsNullOrWhiteSpace(x.Reference))
                    .Select(x => x.Reference!)
                    .ToList();

                if (string.IsNullOrWhiteSpace(idAlegra))
                {
                    resultados.Add(new SetFacturaItemResponse
                    {
                        IdAlegra = idAlegra,
                        ExisteLocalmente = false,
                        Registrada = false,
                        IsOk = false,
                        Mensaje = "Factura de Alegra sin Id",
                        ReferenciasProductos = referencias
                    });
                    facturasConError++;
                    continue;
                }

                if (referencias.Count == 0)
                {
                    resultados.Add(new SetFacturaItemResponse
                    {
                        IdAlegra = idAlegra,
                        ExisteLocalmente = false,
                        Registrada = false,
                        IsOk = false,
                        Mensaje = "Factura sin referencias de productos",
                        ReferenciasProductos = referencias
                    });
                    facturasConError++;
                    continue;
                }

                try
                {
                    GetFacturaResponse facturaLocal = await _mngFacturaL20.GetFactura(new GetFacturaRequest
                    {
                        SessionToken = request.SessionToken,
                        IdFactura = null,
                        IdAlegra = idAlegra
                    });

                    if (facturaLocal.Facturas.Count > 0)
                    {
                        facturasExistentes++;
                        continue;
                    }

                    SetFacturaDbResponse setFactura = await _mngFacturaL20.SetFactura(new SetFacturaDbRequest
                    {
                        SessionToken = request.SessionToken,
                        IdAlegra = idAlegra,
                        Observacion = "Factura importada desde Alegra",
                        ReferenciasProductos = referencias
                    });

                    resultados.Add(new SetFacturaItemResponse
                    {
                        IdAlegra = idAlegra,
                        ExisteLocalmente = false,
                        Registrada = true,
                        IsOk = true,
                        Mensaje = setFactura.Mensaje,
                        IdFacturaLocal = setFactura.IdFactura,
                        ReferenciasProductos = referencias
                    });
                    facturasRegistradas++;
                }
                catch (Exception ex)
                {
                    resultados.Add(new SetFacturaItemResponse
                    {
                        IdAlegra = idAlegra,
                        ExisteLocalmente = false,
                        Registrada = false,
                        IsOk = false,
                        Mensaje = ex.Message,
                        ReferenciasProductos = referencias
                    });
                    facturasConError++;
                }
            }

            return new SetFacturaResponse
            {
                IsOk = true,
                Mensaje = "Proceso de registro de facturas completado",
                FacturasAlegraConsultadas = facturas.Count,
                FacturasExistentes = facturasExistentes,
                FacturasRegistradas = facturasRegistradas,
                FacturasConError = facturasConError,
                Resultados = resultados
            };
        }
    }
}
