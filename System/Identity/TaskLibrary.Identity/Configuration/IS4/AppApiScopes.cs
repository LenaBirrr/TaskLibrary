namespace TaskLibrary.Identity;

using TaskLibrary.Common.Security;
using Duende.IdentityServer.Models;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.CategoriesRead, "Access to categories API - Read data"),
            new ApiScope(AppScopes.CategoriesWrite, "Access to categories API - Write data"),
            new ApiScope(AppScopes.CommentsRead, "Access to categories API - Read data"),
            new ApiScope(AppScopes.CommentsWrite, "Access to categories API - Write data"),
            new ApiScope(AppScopes.NotificationsRead, "Access to categories API - Read data"),
            new ApiScope(AppScopes.ProgrammingLanguagesRead, "Access to categories API - Read data"),
            new ApiScope(AppScopes.ProgrammingLanguagesWrite, "Access to categories API - Write data"),
            new ApiScope(AppScopes.ProgrammingTasksRead, "Access to categories API - Read data"),
            new ApiScope(AppScopes.ProgrammingTasksWrite, "Access to categories API - Write data"),
            new ApiScope(AppScopes.SolutionsRead, "Access to categories API - Read data"),
            new ApiScope(AppScopes.SolutionsWrite, "Access to categories API - Write data"),
            new ApiScope(AppScopes.SubscriptionsRead, "Access to categories API - Read data"),
            new ApiScope(AppScopes.SubscriptionsWrite, "Access to categories API - Write data")
        };
}