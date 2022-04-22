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
    public class AddNotificationModel
    {
        public int? SubscribtionId { get; set; }
        public string Text { get; set; }
    }
    public class AddNotificationModelValidator : AbstractValidator<AddNotificationModel>
    {
        public AddNotificationModelValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Text is required.")
                .MaximumLength(50).WithMessage("Text is long.");
        }
    }

    public class AddNotificationModelProfile : Profile
    {
        public AddNotificationModelProfile()
        {
            CreateMap<AddNotificationModel, Notification>();
        }
    }
}
