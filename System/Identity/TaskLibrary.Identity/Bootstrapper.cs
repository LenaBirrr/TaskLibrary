
namespace TaskLibrary.Identity;

using TaskLibrary.Settings;

public static class Bootstrapper
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services
            .AddSettings()
            ;
    }
}