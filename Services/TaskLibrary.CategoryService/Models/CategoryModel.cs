using AutoMapper;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.CategoryService
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryModelProfile : Profile
    {
        public CategoryModelProfile()
        {
            CreateMap<Category, CategoryModel>();
        }
    }
}
