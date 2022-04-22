using AutoMapper;
using FluentValidation;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.CategoryService
{
    public class UpdateCategoryModel
    {
        public string Name { get; set; }
    }
    public class UpdateCategoryModelValidator : AbstractValidator<UpdateCategoryModel>
    {
        public UpdateCategoryModelValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required.")
               .MaximumLength(50).WithMessage("Name is long.");
        }
    }

    public class UpdateBookModelProfile : Profile
    {
        public UpdateBookModelProfile()
        {
            CreateMap<UpdateCategoryModel, Category>();
        }
    }
}
