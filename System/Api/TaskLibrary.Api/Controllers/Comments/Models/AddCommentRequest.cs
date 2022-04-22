using AutoMapper;
using FluentValidation;
using TaskLibrary.CommentService.Models;

namespace TaskLibrary.Api.Controllers.Comments.Models
{
    public class AddCommentRequest
    {
        public string Text { get; set; }
        
        public int ProgrammingTaskId { get; set; }

        public Guid UserId { get; set; }
    }
    public class AddCommentResponseValidator : AbstractValidator<AddCommentRequest>
    {
        public AddCommentResponseValidator()
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

    public class AddCommentRequestProfile : Profile
    {
        public AddCommentRequestProfile()
        {
            CreateMap<AddCommentRequest, AddCommentModel>();
        }
    }
}
