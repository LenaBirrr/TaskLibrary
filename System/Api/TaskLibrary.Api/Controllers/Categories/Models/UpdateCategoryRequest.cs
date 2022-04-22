using AutoMapper;
using FluentValidation;
using TaskLibrary.CategoryService;

namespace TaskLibrary.Api.Controllers.Categories.Models
{
    public class UpdateCategoryRequest
    {
        public string Name { get; set; }

    }

    public class UpdateCategoryResponseValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryResponseValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");
        }
    }

    public class UpdateCategoryRequestProfile : Profile
    {
        public UpdateCategoryRequestProfile()
        {
            CreateMap<UpdateCategoryRequest, UpdateCategoryModel>();
        }
    }
}
