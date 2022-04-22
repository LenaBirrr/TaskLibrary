using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.CommentService.Models
{
    public class UpdateCommentModel
    {
        public string Text { get; set; }

        public int ProgrammingTaskId { get; set; }

        public Guid UserId { get; set; }

    }

    public class UpdateCommentModelValidator : AbstractValidator<UpdateCommentModel>
    {
        public UpdateCommentModelValidator()
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

    public class UpdateCommentModelProfile : Profile
    {
        public UpdateCommentModelProfile()
        {
            CreateMap<UpdateCommentModel, Comment>();
        }
    }
}
