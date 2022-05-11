using Serilog;
using TaskLibrary.Api;
using TaskLibrary.Api.Configuration;
using TaskLibrary.Api.Configuration.HealthChecks;
using TaskLibrary.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration
    .Enrich.WithCorrelationId()
    .ReadFrom.Configuration(hostBuilderContext.Configuration);
});

var settings = new ApiSettings(new SettingsSource());
// Add services to the container.
var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppDbContext(settings);

services.AddAppHealthCheck();

services.AddAppVersions();

services.AddAppSwagger(settings);

services.AddAppCors();
services.AddAppServices();
services.AddAppAuth(settings);
services.AddControllers().AddValidator();

services.AddRazorPages();




services.AddAutoMappers();

//builder.Services.AddControllers();

var app = builder.Build();

Log.Information("Starting up");

app.UseStaticFiles();
app.UseAppMiddlewares();


app.UseRouting();

app.UseAppCors();




app.UseAppHealthCheck();

app.UseSerilogRequestLogging();

app.MapRazorPages();

app.UseAppAuth();

app.MapControllers();

app.UseAppSwagger();

app.UseAppDbContext();

app.Run();
