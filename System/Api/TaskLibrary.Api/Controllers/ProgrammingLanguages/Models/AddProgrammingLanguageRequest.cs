using AutoMapper;
using FluentValidation;
using TaskLibrary.ProgrammingLanguageService.Models;

namespace TaskLibrary.Api.Controllers.ProgrammingLanguages.Models
{
    public class AddProgrammingLanguageRequest
    {
        public string Name { get; set; }

        public string Level { get; set; }

        public string Paradigm { get; set; }

        public string Realization { get; set; }
    }
    public class AddProgrammingLanguageResponseValidator : AbstractValidator<AddProgrammingLanguageRequest>
    {
        public AddProgrammingLanguageResponseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");
            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("Level is required.")
                .MaximumLength(50).WithMessage("Level is long.");
            RuleFor(x => x.Paradigm)
                .NotEmpty().WithMessage("Paradigm is required.")
                .MaximumLength(50).WithMessage("Paradigm is long.");
            RuleFor(x => x.Realization)
                .NotEmpty().WithMessage("Realization is required.")
                .MaximumLength(50).WithMessage("Realization is long.");
        }
    }

    public class AddProgrammingLanguageRequestProfile : Profile
    {
        public AddProgrammingLanguageRequestProfile()
        {
            CreateMap<AddProgrammingLanguageRequest, AddProgrammingLanguageModel>();
        }
    }
}
