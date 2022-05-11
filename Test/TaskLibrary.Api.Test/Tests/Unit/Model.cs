using TaskLibrary.CategoryService;

namespace TaskLibrary.Api.Test.Tests.Unit
{
    public partial class CategoryUnitTest
    {
        public static UpdateCategoryModel UpdateCategoryModel(string name)
        {
            return new UpdateCategoryModel()
            {

                Name = name
            };
        }
    }
}
