using FrontendPrincipal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo que la sesión permanece activa
    options.Cookie.HttpOnly = true; // Proteger la cookie de la sesión
    options.Cookie.IsEssential = true; // Requerido para cumplir con GDPR
});

// Registrar servicios HTTP para consumir las APIs
builder.Services.AddHttpClient<UsuariosService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiUrls:ReservacionCasasAPI"]);
});

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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
