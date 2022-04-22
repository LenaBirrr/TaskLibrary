using AutoMapper;
using TaskLibrary.CategoryService;

namespace TaskLibrary.Api.Controllers.Categories.Models
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class CategoryResponseProfile : Profile
    {
        public CategoryResponseProfile()
        {
            CreateMap<CategoryModel, CategoryResponse>();
        }
    }
}
