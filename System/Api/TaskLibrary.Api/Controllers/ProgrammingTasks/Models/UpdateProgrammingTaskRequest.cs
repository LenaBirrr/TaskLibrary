using AutoMapper;
using FluentValidation;
using TaskLibrary.ProgrammingTaskService.Models;

namespace TaskLibrary.Api.Controllers.ProgrammingTasks.Models
{
    public class UpdateProgrammingTaskRequest
    {
        public string ProblemStatement { get; set; }

        public string Name { get; set; }

        public string Answers { get; set; }

        public Guid? UserId { get; set; }

        public int? CategoryId { get; set; }
    }
    public class UpdateProgrammingTaskResponseValidator : AbstractValidator<UpdateProgrammingTaskRequest>
    {
        public UpdateProgrammingTaskResponseValidator()
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

    public class UpdateProgrammingTaskModelProfile : Profile
    {
        public UpdateProgrammingTaskModelProfile()
        {
            CreateMap<UpdateProgrammingTaskRequest, ProgrammingTaskModel>();
        }
    }
}
