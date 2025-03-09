using Clinica_Dental.Models;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor de inyecci�n de dependencias.
builder.Services.AddControllersWithViews();

// Inyectar AccesoDatos para que est� disponible en los controladores.
// Se usa AddScoped para crear una instancia por solicitud.
builder.Services.AddScoped<AccesoDatos>();

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HSTS se utiliza para escenarios de producci�n.
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configurar la ruta por defecto: controlador Home y acci�n Index.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
