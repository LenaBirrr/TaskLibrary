namespace TaskLibrary.Identity;

using TaskLibrary.Common.Security;
using Duende.IdentityServer.Models;

public static class AppClients
{
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "swagger",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AccessTokenLifetime = 3600, // 1 hour

                AllowedScopes = {
                    AppScopes.CategoriesRead,
                    AppScopes.CategoriesWrite,
                    AppScopes.CommentsRead,
                    AppScopes.CommentsWrite,
                    AppScopes.ProgrammingLanguagesRead,
                    AppScopes.ProgrammingLanguagesWrite,
                    AppScopes.ProgrammingTasksRead,
                    AppScopes.ProgrammingTasksWrite,
                    AppScopes.SubscriptionsRead,
                    AppScopes.SubscriptionsWrite,
                    AppScopes.SolutionsRead,
                    AppScopes.SolutionsWrite,
                    AppScopes.NotificationsRead
                    

                }
            }
            ,
            new Client
            {
                ClientId = "frontend",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt,

                AccessTokenLifetime = 3600, // 1 hour

                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = 2592000, // 30 days
                SlidingRefreshTokenLifetime = 1296000, // 15 days

                AllowedScopes = {
                    AppScopes.CategoriesRead,
                    AppScopes.CategoriesWrite,
                    AppScopes.CommentsRead,
                    AppScopes.CommentsWrite,
                    AppScopes.ProgrammingLanguagesRead,
                    AppScopes.ProgrammingLanguagesWrite,
                    AppScopes.ProgrammingTasksRead,
                    AppScopes.ProgrammingTasksWrite,
                    AppScopes.SubscriptionsRead,
                    AppScopes.SubscriptionsWrite,
                    AppScopes.SolutionsRead,
                    AppScopes.SolutionsWrite,
                    AppScopes.NotificationsRead
                }
            }
        };
}