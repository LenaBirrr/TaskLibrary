using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.CategoryService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetCategories(int offset = 0, int limit = 10);
        Task<CategoryModel> GetCategory(int id);
        Task<CategoryModel> AddCategory(AddCategoryModel model);
        Task UpdateCategory(int id, UpdateCategoryModel model);
        Task DeleteCategory(int id);
    }
}
