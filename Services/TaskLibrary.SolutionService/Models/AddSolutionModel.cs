using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.SolutionService.Models
{
    public class AddSolutionModel
    {
        public string Text { get; set; }

        public int ProgrammingTaskId { get; set; }
        public Guid? UserId { get; set; }
        public int? ProgrammingLanguageId { get; set; }
    }
    public class AddSolutionModelValidator : AbstractValidator<AddSolutionModel>
    {
        public AddSolutionModelValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required.");
            RuleFor(x => x.ProgrammingTaskId)
                .NotEmpty().WithMessage("Task is required.");
        }
    }

    public class AddSolutionModelProfile : Profile
    {
        public AddSolutionModelProfile()
        {
            CreateMap<AddSolutionModel, Solution>();
        }
    }
}
