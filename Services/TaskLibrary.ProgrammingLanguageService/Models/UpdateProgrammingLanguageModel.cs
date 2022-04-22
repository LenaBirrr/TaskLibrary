using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.ProgrammingLanguageService.Models
{
    public class UpdateProgrammingLanguageModel
    {
        public string Name { get; set; }

        public string Level { get; set; }

        public string Paradigm { get; set; }

        public string Realization { get; set; }
    }

    public class UpdateProgrammingLanguageModelValidator : AbstractValidator<UpdateProgrammingLanguageModel>
    {
        public UpdateProgrammingLanguageModelValidator()
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

    public class UpdateProgrammingLanguageModelProfile : Profile
    {
        public UpdateProgrammingLanguageModelProfile()
        {
            CreateMap<UpdateProgrammingLanguageModel, ProgrammingLanguage>();
        }
    }
}
