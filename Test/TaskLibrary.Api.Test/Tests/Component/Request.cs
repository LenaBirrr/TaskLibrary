using TaskLibrary.Api.Controllers.Categories.Models;

namespace TaskLibrary.Api.Test.Tests.Component
{
    public partial class CategoryIntegrationTest
    {
        public static AddCategoryRequest AddCategoryRequest(string name)
        {
            return new AddCategoryRequest()
            {
                Name = name
            };
        }

        public static UpdateCategoryRequest UpdateCategoryRequest(string name)
        {
            return new UpdateCategoryRequest()
            {
                Name = name

            };
        }
    }
}
