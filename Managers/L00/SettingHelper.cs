using Microsoft.Extensions.Configuration;

namespace PetraConectBack.Managers.L00
{
    public class SettingHelper
    {
        private readonly IConfiguration _configuration;

        public SettingHelper(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetConnectionString()
        {
            string? value = _configuration.GetConnectionString("PostgreSql");
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la cadena de conexión 'PostgreSql' en appsettings.json.");
            return value.Trim();
        }

        public string GetLogPath()
        {
            string? environment = _configuration["AppConfig:Environment"];
            if (string.IsNullOrWhiteSpace(environment))
                throw new Exception("No se encontró la configuración 'AppConfig:Environment' en appsettings.json.");
            string? value;
            if (environment.Trim().Equals("Development", StringComparison.OrdinalIgnoreCase))
            {
                value = _configuration["LoggingConfig:LogPathWindows"];
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("No se encontró la ruta de log 'LoggingConfig:LogPathWindows' en appsettings.json.");
                return value.Trim();
            }
            value = _configuration["LoggingConfig:LogPath"];
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la ruta de log 'LoggingConfig:LogPath' en appsettings.json.");
            return value.Trim();
        }

        public string GetLogPathLinux()
        {
            string? value = _configuration["LoggingConfig:LogPath"];
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la ruta de log 'LoggingConfig:LogPath' en appsettings.json.");
            return value.Trim();
        }

        public string GetLogPathWindows()
        {
            string? value = _configuration["LoggingConfig:LogPathWindows"];
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la ruta de log 'LoggingConfig:LogPathWindows' en appsettings.json.");
            return value.Trim();
        }

        public string GetEnvironment()
        {
            string? value = _configuration["AppConfig:Environment"];
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la configuración 'AppConfig:Environment' en appsettings.json.");
            return value.Trim();
        }

        public int GetSessionMinutes()
        {
            string? value = _configuration["SessionConfig:MinutosCaduca"];
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la configuración 'SessionConfig:MinutosCaduca' en appsettings.json.");
            if (!int.TryParse(value, out int minutes))
                throw new Exception("El valor 'SessionConfig:MinutosCaduca' debe ser numérico.");
            if (minutes <= 0)
                throw new Exception("El valor 'SessionConfig:MinutosCaduca' debe ser mayor que cero.");
            return minutes;
        }

        public string GetAlegraBaseUrl()
        {
            string? value = _configuration["AlegraConfig:BaseUrl"];
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la configuración 'AlegraConfig:BaseUrl' en appsettings.json.");
            return value.Trim().TrimEnd('/');
        }

        public string GetAlegraItemsEndpoint()
        {
            string? value = _configuration["AlegraConfig:ItemsEndpoint"];
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la configuración 'AlegraConfig:ItemsEndpoint' en appsettings.json.");
            value = value.Trim();
            if (!value.StartsWith("/"))
                value = "/" + value;
            return value;
        }

        public string GetAlegraInvoicesEndpoint()
        {
            string? value = _configuration["AlegraConfig:InvoicesEndpoint"];
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("No se encontró la configuración 'AlegraConfig:InvoicesEndpoint' en appsettings.json.");
            value = value.Trim();
            if (!value.StartsWith("/"))
                value = "/" + value;
            return value;
        }

        public string GetAlegraItemsUrl()
        {
            string? baseUrl = _configuration["AlegraConfig:BaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new Exception("No se encontró la configuración 'AlegraConfig:BaseUrl' en appsettings.json.");
            string? endpoint = _configuration["AlegraConfig:ItemsEndpoint"];
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new Exception("No se encontró la configuración 'AlegraConfig:ItemsEndpoint' en appsettings.json.");
            baseUrl = baseUrl.Trim().TrimEnd('/');
            endpoint = endpoint.Trim();
            if (!endpoint.StartsWith("/"))
                endpoint = "/" + endpoint;
            return $"{baseUrl}{endpoint}";
        }

        public string GetAlegraInvoicesUrl()
        {
            string? baseUrl = _configuration["AlegraConfig:BaseUrl"];
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new Exception("No se encontró la configuración 'AlegraConfig:BaseUrl' en appsettings.json.");
            string? endpoint = _configuration["AlegraConfig:InvoicesEndpoint"];
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new Exception("No se encontró la configuración 'AlegraConfig:InvoicesEndpoint' en appsettings.json.");
            baseUrl = baseUrl.Trim().TrimEnd('/');
            endpoint = endpoint.Trim();
            if (!endpoint.StartsWith("/"))
                endpoint = "/" + endpoint;
            return $"{baseUrl}{endpoint}";
        }

        public int GetAlegraLastFactLimit()
        {
            string? value = _configuration["AlegraConfig:LastFactLimit"];
            if (string.IsNullOrWhiteSpace(value))
                return 10;
            if (!int.TryParse(value, out int limit))
                throw new Exception("El valor 'AlegraConfig:LastFactLimit' debe ser numérico.");
            if (limit <= 0)
                return 10;
            if (limit > 30)
                return 30;
            return limit;
        }

    }
}