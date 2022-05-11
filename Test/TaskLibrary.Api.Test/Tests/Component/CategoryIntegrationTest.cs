using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Api.Test.Common;

namespace TaskLibrary.Api.Test.Tests.Component
{
    [TestFixture]
    public partial class CategoryIntegrationTest : ComponentTest
    {
        [SetUp]
        public async Task SetUp()
        {
            await using var context = await DbContext();

            context.Categories.RemoveRange(context.Categories);
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();


            var category = new Db.Entities.Category()
            {
                Name = "Для начинающих"
            };
            context.Categories.Add(category);

            category = new Db.Entities.Category()
            {
                Name = "Для продвинутых"
            };
            context.Categories.Add(category);

            context.SaveChanges();
        }

        [TearDown]
        public async override Task TearDown()
        {
            await using var context = await DbContext();
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();
            await base.TearDown();
        }

        protected static class Urls
        {
            public static string GetCategories(int? offset = null, int? limit = null)
            {

                if (offset is null && limit is null)
                    return $"/api/v1/categories";
                List<string> queryParameters = new List<string>();

                if (offset.HasValue)
                {
                    queryParameters.Add($"offset={offset}");
                }

                if (limit.HasValue)
                {
                    queryParameters.Add($"limit={limit}");
                }

                var queryString = string.Join("&", queryParameters);
                return $"/api/v1/categories?{queryString}";
            }

            public static string GetCategory(string id) => $"/api/v1/categories/{id}";

            public static string DeleteCategory(string id) => $"/api/v1/categories/{id}";

            public static string UpdateCategory(string id) => $"/api/v1/categories/{id}";

            public static string AddCategory => $"/api/v1/categories";
        }

        public static class Scopes
        {
            public static string ReadCategories => "offline_access categories_read";

            public static string WriteCategories => "offline_access categories_write";

            public static string ReadAndWriteCategories => "offline_access categories_read categories_write";

            public static string Empty => "offline_access";
        }

        public async Task<string> AuthenticateUser_ReadAndWriteCategoriesScope()
        {
            var user = GetTestUser();
            var tokenResponse = await AuthenticateTestUser(user.Username, user.Password, Scopes.ReadAndWriteCategories);
            return tokenResponse.AccessToken;
        }

        public async Task<string> AuthenticateUser_EmptyScope()
        {
            var user = GetTestUser();
            /*user.Username= "tasklibraryemail1@gmail.com";
            user.Password = "pass";*/
             var tokenResponse = await AuthenticateTestUser(user.Username, user.Password, Scopes.Empty);
            return tokenResponse.AccessToken;
        }

    }
}
