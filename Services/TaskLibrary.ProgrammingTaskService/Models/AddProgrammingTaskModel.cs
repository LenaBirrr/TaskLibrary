using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.ProgrammingTaskService.Models
{
    public class AddProgrammingTaskModel
    {
        public string ProblemStatement { get; set; }

        public string Name { get; set; }

        public string Answers { get; set; }        

        public Guid? UserId { get; set; }

        public int? CategoryId { get; set; }
    }

    public class AddProgrammingTaskModelValidator : AbstractValidator<AddProgrammingTaskModel>
    {
        public AddProgrammingTaskModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name is long.");
            RuleFor(x => x.ProblemStatement)
                .NotEmpty().WithMessage("Problem Statement is required.")
                .MaximumLength(50).WithMessage("Problem Statement is long.");
            RuleFor(x => x.Answers)
                .NotEmpty().WithMessage("Answers are required.")
                .MaximumLength(50).WithMessage("Answers are long.");
        }
    }

    public class AddProgrammingTaskModelProfile : Profile
    {
        public AddProgrammingTaskModelProfile()
        {
            CreateMap<AddProgrammingTaskModel, ProgrammingTask>();
        }
    }
}
