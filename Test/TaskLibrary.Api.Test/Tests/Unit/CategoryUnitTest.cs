using System.Linq;
using System.Threading.Tasks;
using TaskLibrary.Api.Test.Common;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.Api.Test.Tests.Unit
{
    public partial class CategoryUnitTest : UnitTest
    {
        

        public async Task<int> GetExistedCategoryId()
        {
            await using var context = await DbContext();
            
            await using var context1 = await DbContext();
            var cat1 = context1.Categories.AsEnumerable().First();
            return cat1.Id;
        }


        public async Task<Category> GetCategoryById(int id)
        {
            await using var context = await DbContext();
            var book = context.Categories.FirstOrDefault(x => x.Id == id);
            return book;
        }

        protected async override Task ClearDb()
        {
            await using var context = await DbContext();
            context.Categories.RemoveRange(context.Categories);
            context.SaveChanges();
        }
    }
}
