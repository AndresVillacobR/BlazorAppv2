using BlazorAppv2.Components;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar Supabase
builder.Services.AddScoped(provider =>
{
    var url = builder.Configuration["Supabase:Url"];
    var key = builder.Configuration["Supabase:Key"];
    
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