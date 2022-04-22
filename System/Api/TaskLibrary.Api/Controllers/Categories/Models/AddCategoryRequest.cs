namespace TaskLibrary.Api.Controllers.Categories.Models
{
    using AutoMapper;
    using FluentValidation;
    using TaskLibrary.CategoryService;

    public class AddCategoryRequest
    {
        public string Name { get; set; }
        
    }

    public class AddCategoryResponseValidator : AbstractValidator<AddCategoryRequest>
    {
        public AddCategoryResponseValidator()
        {
           
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");
        }
    }

    public class AddCategoryRequestProfile : Profile
    {
        public AddCategoryRequestProfile()
        {
            CreateMap<AddCategoryRequest, AddCategoryModel>();
        }
    }

}
