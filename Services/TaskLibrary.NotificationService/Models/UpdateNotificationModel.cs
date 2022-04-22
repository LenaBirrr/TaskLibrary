using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.NotificationService.Models
{
    public class UpdateNotificationModel
    {
        public int? SubscribtionId { get; set; }
        public string Text { get; set; }
    }
    public class UpdateNotificationModelValidator : AbstractValidator<UpdateNotificationModel>
    {
        public UpdateNotificationModelValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required.")
                .MaximumLength(50).WithMessage("Text is long.");
        }
    }

    public class UpdateNotificationModelProfile : Profile
    {
        public UpdateNotificationModelProfile()
        {
            CreateMap<UpdateNotificationModel, Notification>();
        }
    }
}
