using MVC_BOCHA_STORE.Service;
using Azure.Messaging.ServiceBus;
using static MVC_BOCHA_STORE.Service.ServiceBusService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAPIServiceProovedor, APIServiceProovedor>();
builder.Services.AddScoped<IAPIServiceMarca, APIServiceMarca>();
builder.Services.AddScoped<IAPIServiceProducto, APIServiceProducto>();
builder.Services.AddScoped<IAPIServiceUsuario, APIServiceUsuario>();

// Configuraci�n de Azure Service Bus
var serviceBusSettings = builder.Configuration.GetSection("ServiceBusQueues");
string serviceBusConnectionString = builder.Configuration.GetConnectionString("ServiceBusConnectionString");
string productsQueueName = serviceBusSettings["ProductsQueueName"];
string suppliersQueueName = serviceBusSettings["SuppliersQueueName"];
string brandsQueueName = serviceBusSettings["BrandsQueueName"];
builder.Services.AddSingleton<IServiceBusService>(new ServiceBusService(serviceBusConnectionString, productsQueueName, suppliersQueueName, brandsQueueName));


// Agregar el servicio de sesi�n aqu�
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Establecer un tiempo de espera de la sesi�n, por ejemplo, 40 minutos
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

// Agregar el middleware de sesi�n aqu�
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=SignIn}/{id?}");

app.Run();