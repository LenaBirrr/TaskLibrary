using TaskLibrary.Common.Helpers;

namespace TaskLibrary.Api.Configuration
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        {
            AutoMappersRegisterHelper.Register(services);

            return services;
        }
    }
}
