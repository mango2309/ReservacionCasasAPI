using Microsoft.EntityFrameworkCore;
using SistemaLavanderiasAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de los servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Asegúrate de tener la cadena de conexión en el archivo appsettings.json

builder.Services.AddControllers();

// Agregar servicios Swagger para documentación de la API (opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración de la canalización de solicitudes HTTP

// Habilitar Swagger (opcional)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();