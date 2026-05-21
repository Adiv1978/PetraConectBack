using System.Net.Http.Headers;
using System.Text;

namespace PetraConectBack.Managers.L00
{
    public class AlegraHelper
    {
        private readonly string _itemsUrl;
        private readonly string _invoicesUrl;
        private readonly int _timeoutSeconds;

        public AlegraHelper(
            string itemsUrl,
            string invoicesUrl,
            int timeoutSeconds = 30)
        {
            if (string.IsNullOrWhiteSpace(itemsUrl))
                throw new ArgumentException("La URL del endpoint de items de Alegra no puede estar vacía.", nameof(itemsUrl));
            if (string.IsNullOrWhiteSpace(invoicesUrl))
                throw new ArgumentException("La URL del endpoint de invoices de Alegra no puede estar vacía.", nameof(invoicesUrl));
            if (timeoutSeconds <= 0)
                throw new ArgumentException("El timeout debe ser mayor que cero.", nameof(timeoutSeconds));
            _itemsUrl = itemsUrl.Trim();
            _invoicesUrl = invoicesUrl.Trim();
            _timeoutSeconds = timeoutSeconds;
        }

        public async Task<string> GetItemsAsync(
            string emailAlegra,
            string keyAlegra,
            Dictionary<string, string?>? queryParams = null)
        {
            if (string.IsNullOrWhiteSpace(emailAlegra))
                throw new ArgumentException("El email de Alegra no puede estar vacío.", nameof(emailAlegra));
            if (string.IsNullOrWhiteSpace(keyAlegra))
                throw new ArgumentException("La key de Alegra no puede estar vacía.", nameof(keyAlegra));
            if (queryParams != null && queryParams.ContainsKey("limit"))
            {
                string? limitValue = queryParams["limit"];
                if (!string.IsNullOrWhiteSpace(limitValue))
                {
                    if (!int.TryParse(limitValue, out int limit))
                        throw new ArgumentException("El parámetro limit debe ser numérico.");
                    if (limit > 30)
                        throw new ArgumentException("El límite máximo permitido por Alegra para items es 30.");
                    if (limit <= 0)
                        throw new ArgumentException("El parámetro limit debe ser mayor que cero.");
                }
            }
            string finalUrl = _itemsUrl;
            if (queryParams != null && queryParams.Count > 0)
            {
                List<string> queryParts = new List<string>();
                foreach (KeyValuePair<string, string?> parameter in queryParams)
                {
                    if (!string.IsNullOrWhiteSpace(parameter.Key) &&
                        !string.IsNullOrWhiteSpace(parameter.Value))
                    {
                        string key = Uri.EscapeDataString(parameter.Key.Trim());
                        string value = Uri.EscapeDataString(parameter.Value.Trim());
                        queryParts.Add($"{key}={value}");
                    }
                }
                if (queryParts.Count > 0)
                {
                    string separator = finalUrl.Contains("?") ? "&" : "?";
                    finalUrl = finalUrl + separator + string.Join("&", queryParts);
                }
            }
            using HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(_timeoutSeconds);
            string credentials = $"{emailAlegra.Trim()}:{keyAlegra.Trim()}";
            string credentialsBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentialsBase64);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            using HttpResponseMessage response = await httpClient.GetAsync(finalUrl);
            string responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error al consultar items en Alegra. StatusCode: {(int)response.StatusCode}. Respuesta: {responseBody}");
            }
            return responseBody;
        }

        public async Task<string> GetInvoicesAsync(
            string emailAlegra,
            string keyAlegra,
            Dictionary<string, string?>? queryParams = null)
        {
            if (string.IsNullOrWhiteSpace(emailAlegra))
                throw new ArgumentException("El email de Alegra no puede estar vacío.", nameof(emailAlegra));
            if (string.IsNullOrWhiteSpace(keyAlegra))
                throw new ArgumentException("La key de Alegra no puede estar vacía.", nameof(keyAlegra));
            if (queryParams != null && queryParams.ContainsKey("limit"))
            {
                string? limitValue = queryParams["limit"];
                if (!string.IsNullOrWhiteSpace(limitValue))
                {
                    if (!int.TryParse(limitValue, out int limit))
                        throw new ArgumentException("El parámetro limit debe ser numérico.");
                    if (limit > 30)
                        throw new ArgumentException("El límite máximo permitido por Alegra para facturas es 30.");
                    if (limit <= 0)
                        throw new ArgumentException("El parámetro limit debe ser mayor que cero.");
                }
            }
            string finalUrl = _invoicesUrl;
            if (queryParams != null && queryParams.Count > 0)
            {
                List<string> queryParts = new List<string>();
                foreach (KeyValuePair<string, string?> parameter in queryParams)
                {
                    if (!string.IsNullOrWhiteSpace(parameter.Key) &&
                        !string.IsNullOrWhiteSpace(parameter.Value))
                    {
                        string key = Uri.EscapeDataString(parameter.Key.Trim());
                        string value = Uri.EscapeDataString(parameter.Value.Trim());
                        queryParts.Add($"{key}={value}");
                    }
                }
                if (queryParts.Count > 0)
                {
                    string separator = finalUrl.Contains("?") ? "&" : "?";
                    finalUrl = finalUrl + separator + string.Join("&", queryParts);
                }
            }
            using HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(_timeoutSeconds);
            string credentials = $"{emailAlegra.Trim()}:{keyAlegra.Trim()}";
            string credentialsBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", credentialsBase64);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            using HttpResponseMessage response = await httpClient.GetAsync(finalUrl);
            string responseBody = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException(
                    $"Error al consultar facturas en Alegra. StatusCode: {(int)response.StatusCode}. Respuesta: {responseBody}");
            return responseBody;
        }
    }
}
