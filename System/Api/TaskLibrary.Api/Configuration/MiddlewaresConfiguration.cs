using TaskLibrary.Api.Middlewares;

namespace TaskLibrary.Api.Configuration
{
    public static class MiddlewaresConfiguration
    {
        public static IApplicationBuilder UseAppMiddlewares(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
