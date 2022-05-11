using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaskLibrary.Api.Test.Common.Extentions;

namespace TaskLibrary.Api.Test.Tests.Component
{
    public partial class CategoryIntegrationTest
    {

        [Test]
        public async Task AddCategory_ValidParameters_Authenticated_OkResponse()
        {
            var accessToken = await AuthenticateUser_ReadAndWriteCategoriesScope();
            var url = Urls.AddCategory;

            
            var request = AddCategoryRequest(Generator.ValidNames.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


            await using var context = await DbContext();
            var newCategory = context.Categories.AsEnumerable().OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.IsNotNull(newCategory);

            Assert.AreEqual(request.Name, newCategory?.Name);
        }

       
        [Test]
        [TestCaseSource(typeof(Generator), nameof(Generator.InvalidNames))]
        public async Task AddCategory_InvalidName_Authenticated_BadRequest(string name)
        {
            var accessToken = await AuthenticateUser_ReadAndWriteCategoriesScope();
            var url = Urls.AddCategory;

            var request = AddCategoryRequest(name);
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

       
        [Test]
        public async Task AddBook_Unauthorized()
        {
            var url = Urls.AddCategory;

            var request = AddCategoryRequest(Generator.ValidNames.First());
            var response = await apiClient.PostJson(url, request, null);
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task AddBook_Forbidden()
        {
            var accessToken = await AuthenticateUser_EmptyScope();
            var url = Urls.AddCategory;

            var request = AddCategoryRequest(Generator.ValidNames.First());
            var response = await apiClient.PostJson(url, request, accessToken);
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
