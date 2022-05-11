using NUnit.Framework;
using System.Threading.Tasks;
using TaskLibrary.CategoryService;

namespace TaskLibrary.Api.Test.Tests.Unit
{
    [TestFixture]
    public partial class CategoryUnitTest
    {
        [Test]
        public async Task UpdateCategory_ValidParameters_Success()
        {
            var bookService = services.Get<ICategoryService>();

            var bookId = await GetExistedCategoryId();

            var model = UpdateCategoryModel("test");

            await bookService.UpdateCategory(bookId, model);

            var resultCategory = await GetCategoryById(bookId);

            Assert.IsNotNull(resultCategory);

            Assert.AreEqual(model.Name, resultCategory.Name);
        }
    }
}
