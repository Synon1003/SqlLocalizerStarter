using SqlLocalizerStarter.Web.Components;
using Microsoft.EntityFrameworkCore;
using Localization.SqlLocalizer.DbStringLocalizer;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("LocalizationDb");
builder.Services.AddDbContext<LocalizationModelContext>(options =>
    {
        options.UseSqlServer(connectionString);
        options.LogTo(Console.WriteLine, LogLevel.Information); 
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors(true);
    },
    ServiceLifetime.Singleton,
    ServiceLifetime.Singleton
);
builder.Services.AddSqlLocalization(options => {
    options.UseOnlyPropertyNames = false;
    options.ReturnOnlyKeyIfNotFound = true;
    options.UseTypeFullNames = false;
});

builder.Services.AddLocalization();

var app = builder.Build();

var supportedCultures = new[] { "en", "hu" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

using (var scope = app.Services.CreateScope())
{
    var localizationDb = scope.ServiceProvider.GetRequiredService<LocalizationModelContext>();
    localizationDb.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapDefaultEndpoints();

app.Run();