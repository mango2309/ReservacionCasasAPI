using Microsoft.EntityFrameworkCore;
using SistemaLavanderiasAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de los servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Aseg�rate de tener la cadena de conexi�n en el archivo appsettings.json

builder.Services.AddControllers();

// Agregar servicios Swagger para documentaci�n de la API (opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuraci�n de la canalizaci�n de solicitudes HTTP

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