using BlazorAppv2.Components;
using BlazorAppv2.Models;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar Supabase usando appsettings.json
var url = builder.Configuration["Supabase:Url"];
var key = builder.Configuration["Supabase:Key"];

if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("Supabase URL and Key must be configured in appsettings.json");
}

var supabaseClient = new Client(url, key, new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
});

// ...existing code...
try
{
    var test = supabaseClient
     .From<RiegoItem>()
     .Select("id")
     .Limit(1)
     .Get()
     .GetAwaiter()
     .GetResult();

    if (test.ResponseMessage.IsSuccessStatusCode)
    {
        Console.WriteLine("✅ Conexión a Supabase exitosa");
    }
    else
    {
        Console.WriteLine("❌ Error de conexión a Supabase: " + test.ResponseMessage.StatusCode);
    }
}
catch (Exception ex)
{
    Console.WriteLine("❌ Error de conexión a Supabase: " + ex.Message);
}
// ...existing code...

// Registrar el cliente como singleton
builder.Services.AddSingleton(supabaseClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();