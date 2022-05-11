using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Api.Controllers.Categories.Models;
using TaskLibrary.Api.Test.Common;
using TaskLibrary.Api.Test.Common.Extentions;

namespace TaskLibrary.Api.Test.Tests.Component
{
    public partial class CategoryIntegrationTest
    {
        [Test]
        public async Task GetCategories_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteCategoriesScope();
            var url = Urls.GetCategories();
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var books_from_api = await response.ReadAsObject<IEnumerable<CategoryResponse>>();

            await using var context = await DbContext();
            var books_from_db = context.Categories.AsEnumerable();

            Assert.AreEqual(books_from_db.Count(), books_from_api.Count());
        }

        [Test]
        public async Task GetCategories_NegativeParameters_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteCategoriesScope();
            var url = Urls.GetCategories(-1, -1);
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var categories_from_api = await response.ReadAsObject<IEnumerable<CategoryResponse>>();

            Assert.AreEqual(0, categories_from_api.Count());
        }

        [Test]
        public async Task GetCategories_Unauthorized()
        {
            var url = Urls.GetCategories();
            var response = await apiClient.Get(url);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task GetCategories_EmptyScope_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.GetCategories();
            var response = await apiClient.Get(url, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
