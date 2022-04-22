using AutoMapper;
using FluentValidation;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.CommentService.Models
{
    public class AddCommentModel
    {
        public string Text { get; set; }

        public int ProgrammingTaskId { get; set; }

        public Guid UserId { get; set; }

    }

    public class AddCommentModelValidator : AbstractValidator<AddCommentModel>
    {
        public AddCommentModelValidator()
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

    public class AddCommentModelProfile : Profile
    {
        public AddCommentModelProfile()
        {
            CreateMap<AddCommentModel, Comment>();
        }
    }
}
