using AutoMapper;
using FluentValidation;
using TaskLibrary.SolutionService.Models;

namespace TaskLibrary.Api.Controllers.Solutions.Models
{
    public class UpdateSolutionRequest
    {
        public string Text { get; set; }
        public int ProgrammingTaskId { get; set; }

        public string AuthorName { get; set; }

        public Guid? UserId { get; set; }
        public string ProgrammingTaskName { get; set; }

        public string ProgrammingLanguageName { get; set; }


        public int? ProgrammingLanguageId { get; set; }
    }

    public class UpdateSolutionResponseValidator : AbstractValidator<UpdateSolutionRequest>
    {
        public UpdateSolutionResponseValidator()
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

    public class UpdateSolutionRequestProfile : Profile
    {
        public UpdateSolutionRequestProfile()
        {
            CreateMap<UpdateSolutionRequest, UpdateSolutionModel>();
        }
    }
}
