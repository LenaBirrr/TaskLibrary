using Serilog;
using TaskLibrary.Settings;
using TaskLibrary.Worker;
using TaskLibrary.Worker.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var settings = new WorkerSettings(new SettingsSource());

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog(logger, true);

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppHealthCheck();
services.RegisterServices();


var app = builder.Build();

app.UseAppHealthCheck();

app.StartTaskExecutor();


app.Run();
