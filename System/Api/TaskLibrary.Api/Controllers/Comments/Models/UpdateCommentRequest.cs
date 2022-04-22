using AutoMapper;
using FluentValidation;
using TaskLibrary.CommentService.Models;

namespace TaskLibrary.Api.Controllers.Comments.Models
{
    public class UpdateCommentRequest
    {
        public string Text { get; set; }

        public int ProgrammingTaskId { get; set; }

        public Guid UserId { get; set; }
    }
    public class UpdateCommentResponseValidator : AbstractValidator<UpdateCommentRequest>
    {
        public UpdateCommentResponseValidator()
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

    public class UpdateCommentRequestProfile : Profile
    {
        public UpdateCommentRequestProfile()
        {
            CreateMap<UpdateCommentRequest, UpdateCommentModel>();
        }
    }
}
