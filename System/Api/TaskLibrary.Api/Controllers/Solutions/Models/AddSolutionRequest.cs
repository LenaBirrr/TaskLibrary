using AutoMapper;
using FluentValidation;
using TaskLibrary.SolutionService.Models;

namespace TaskLibrary.Api.Controllers.Solutions.Models
{
    public class AddSolutionRequest
    {
        public string Text { get; set; }
        public int ProgrammingTaskId { get; set; }

        public Guid UserId { get; set; }

        public int? ProgrammingLanguageId { get; set; }
    }
    public class AddSolutionResponseValidator : AbstractValidator<AddSolutionRequest>
    {
        public AddSolutionResponseValidator()
        {

            RuleFor(x => x.ProgrammingTaskId)
             .NotEmpty().WithMessage("Programming task is required.");

            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User is required.");

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required.")
                .MaximumLength(500).WithMessage("Text is long.");
        }
    }

    public class AddSolutionRequestProfile : Profile
    {
        public AddSolutionRequestProfile()
        {
            CreateMap<AddSolutionRequest, AddSolutionModel>();
        }
    }
}
