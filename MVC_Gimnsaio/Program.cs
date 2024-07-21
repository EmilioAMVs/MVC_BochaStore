using MVC_BOCHA_STORE.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAPIServiceProovedor, APIServiceProovedor>();
builder.Services.AddScoped<IAPIServiceMarca, APIServiceMarca>();
builder.Services.AddScoped<IAPIServiceProducto, APIServiceProducto>();
builder.Services.AddScoped<IAPIServiceUsuario, APIServiceUsuario>();

// Agregar el servicio de sesión aquí
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Establecer un tiempo de espera de la sesión, por ejemplo, 40 minutos
    options.IdleTimeout = TimeSpan.FromMinutes(40);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Agregar el middleware de sesión aquí
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=SignIn}/{id?}");

app.Run();