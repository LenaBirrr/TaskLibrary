using TaskLibrary.Identity;
using TaskLibrary.Identity.Configuration;

using TaskLibrary.Settings;

var builder = WebApplication.CreateBuilder(args);
var settings = new IS4Settings(new SettingsSource());
// Add services to the container.

var services = builder.Services;

services.AddAppCors();
services.AddAppDbContext(settings.Db);
services.AddHttpContextAccessor();
services.AddAppServices();
services.AddIS4();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAppCors();
app.UseStaticFiles();
app.UseRouting();
app.UseAppDbContext();
app.UseIS4();
app.Run();
