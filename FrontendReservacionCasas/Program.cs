using FrontendReservacionCasas.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores con vistas
builder.Services.AddControllersWithViews();

// Registrar servicios HTTP para consumir las APIs
builder.Services.AddHttpClient<ReservacionService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:ReservacionCasasAPI"]);
});

builder.Services.AddHttpClient<LavanderiaService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:SistemaLavanderiasAPI"]);
});

builder.Services.AddHttpClient<TiendaService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:TiendasAPI"]);
});

var app = builder.Build();

// Configurar el pipeline de la aplicación
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();