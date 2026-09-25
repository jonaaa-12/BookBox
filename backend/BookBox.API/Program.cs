using DotNetEnv;
using BookBox.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar variables de entorno
Env.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresConnection") 
                       ?? builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Falta la cadena de conexión.");

// 2. Registrar Servicios en el Contenedor
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 3. Construir la Aplicación
var app = builder.Build();

// 4. Configurar Middleware y Rutas HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

// 5. Iniciar el servidor (Siempre al final)
app.Run();