using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using TaskLibrary.Db.Context.Context;
using TaskLibrary.Db.Entities;
using TaskLibrary.Settings;
using Microsoft.IdentityModel.Tokens;


using TaskLibrary.Common.Security;


namespace TaskLibrary.Api.Configuration
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAppAuth(this IServiceCollection services, IApiSettings settings)
        {
            services
                .AddIdentity<User, IdentityRole<Guid>>(opt =>
                {
                    opt.Password.RequiredLength = 0;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<MainDbContext>()
                .AddUserManager<UserManager<User>>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = settings.IdentityServer.RequireHttps;
                    options.Authority = settings.IdentityServer.Url;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = false,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Audience = "api";
                });


            services.AddAuthorization(options =>
            {
                options.AddPolicy(AppScopes.CategoriesRead, policy => policy.RequireClaim("scope", AppScopes.CategoriesRead));
                options.AddPolicy(AppScopes.CategoriesWrite, policy => policy.RequireClaim("scope", AppScopes.CategoriesWrite));
                options.AddPolicy(AppScopes.CommentsRead, policy => policy.RequireClaim("scope", AppScopes.CommentsRead));
                options.AddPolicy(AppScopes.CommentsWrite, policy => policy.RequireClaim("scope", AppScopes.CommentsWrite));
                options.AddPolicy(AppScopes.NotificationsRead, policy => policy.RequireClaim("scope", AppScopes.NotificationsRead));
                options.AddPolicy(AppScopes.ProgrammingLanguagesRead, policy => policy.RequireClaim("scope", AppScopes.ProgrammingLanguagesRead));
                options.AddPolicy(AppScopes.ProgrammingLanguagesWrite, policy => policy.RequireClaim("scope", AppScopes.ProgrammingLanguagesWrite));
                options.AddPolicy(AppScopes.ProgrammingTasksRead, policy => policy.RequireClaim("scope", AppScopes.ProgrammingTasksRead));
                options.AddPolicy(AppScopes.ProgrammingTasksWrite, policy => policy.RequireClaim("scope", AppScopes.ProgrammingTasksWrite));
                options.AddPolicy(AppScopes.SolutionsRead, policy => policy.RequireClaim("scope", AppScopes.SolutionsRead));
                options.AddPolicy(AppScopes.SolutionsWrite, policy => policy.RequireClaim("scope", AppScopes.SolutionsWrite));
                options.AddPolicy(AppScopes.SubscriptionsRead, policy => policy.RequireClaim("scope", AppScopes.SubscriptionsRead));
                options.AddPolicy(AppScopes.SubscriptionsWrite, policy => policy.RequireClaim("scope", AppScopes.SubscriptionsWrite));
            });

            return services;
        }

        public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
