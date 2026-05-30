namespace PetraConectBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Configurar la política de CORS para permitir acceso abierto
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AccesoTotal", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.MapGet("/", () => "PetraConectBack API activa");

            // 2. Habilitar CORS en el pipeline HTTP
            // Debe colocarse obligatoriamente ANTES de UseAuthorization y del mapeo de controladores
            app.UseCors("AccesoTotal");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}