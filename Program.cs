
using System.Net;

namespace PetraConectBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(8080); // HTTP
                                            // options.ListenAnyIP(8081, listenOptions =>
                                            // {
                                            //     listenOptions.UseHttps(); // HTTPS opcional
                                            // });
            });

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.Use(async (context, next) =>
            {
                var remoteIp = context.Connection.RemoteIpAddress;

                if (remoteIp != null && remoteIp.IsIPv4MappedToIPv6)
                    remoteIp = remoteIp.MapToIPv4();

                Console.WriteLine($"IP cliente detectada: {remoteIp}");

                if (remoteIp == null || !IsAllowedIp(remoteIp))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync($"Acceso denegado por IP: {remoteIp}");
                    return;
                }

                await next();
            });

            app.UseAuthorization();
            app.MapGet("/", () => "PetraConectBack API activa en puerto 8080");

            app.MapControllers();

            app.Run();
        }

        private static bool IsAllowedIp(IPAddress ip)
        {
            var bytes = ip.GetAddressBytes();

            if (bytes.Length != 4)
                return false;

            return
                // Red local 192.168.0.0/24
                (bytes[0] == 192 && bytes[1] == 168 && bytes[2] == 0)

                // Localhost Windows
                || ip.ToString() == "127.0.0.1"

                // Android Emulator normalmente llega como 10.0.2.16
                || (bytes[0] == 10 && bytes[1] == 0 && bytes[2] == 2);
        }
    }
}


