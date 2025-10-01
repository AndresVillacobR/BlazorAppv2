using BlazorAppv2.Components;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar Supabase
builder.Services.AddScoped(provider =>
{
    var url = builder.Configuration["https://rddaujeuraknqrmrhsur.supabase.co"];
    var key = builder.Configuration["eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InJkZGF1amV1cmFrbnFybXJoc3VyIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc1ODY2OTE0OSwiZXhwIjoyMDc0MjQ1MTQ5fQ.f_xBoOjFM3vXOBgG0QbmUUFNzdUqJOwl8x9squPNt4g"];
    
    if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
    {
        throw new InvalidOperationException("Supabase URL and Key must be configured in appsettings.json");
    }
    
    return new Client(url, key, new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    });
});

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