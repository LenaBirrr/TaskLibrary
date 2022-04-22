using AutoMapper;
using FluentValidation;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.CategoryService
{

    public class AddCategoryModel
    {
        public string Name { get; set; }

    }

    public class AddCategoryModelValidator : AbstractValidator<AddCategoryModel>
    {
        public AddCategoryModelValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(50).WithMessage("Name is long.");
            
        }
    }

    public class AddCategoryModelProfile : Profile
    {
        public AddCategoryModelProfile()
        {
            CreateMap<AddCategoryModel, Category>();
        }
    }

}
