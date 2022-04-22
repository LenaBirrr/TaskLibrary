using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.SubscriptionService.Models
{
    public class AddSubscriptionModel
    {
        public int ProgrammingTaskId { get; set; }
        public Guid UserId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }

    public class AddSubscriptionModelValidator : AbstractValidator<AddSubscriptionModel>
    {
        public AddSubscriptionModelValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User is required.");
            RuleFor(x => x.ProgrammingTaskId)
                .NotEmpty().WithMessage("Task is required.");
            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("StartTime is required.");
            RuleFor(x => x.FinishTime)
                .NotEmpty().WithMessage("FinishTime is required.");
        }
    }

    public class AddSubscriptionModelProfile : Profile
    {
        public AddSubscriptionModelProfile()
        {
            CreateMap<AddSubscriptionModel, Subscription>();
        }
    }
}
