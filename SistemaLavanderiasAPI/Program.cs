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



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7000") // Cambia al puerto HTTPS del frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

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