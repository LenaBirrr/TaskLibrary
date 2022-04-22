using AutoMapper;
using TaskLibrary.SubscriptionService.Models;

namespace TaskLibrary.Api.Controllers.Subscriptions.Models
{
    public class UpdateSubscriptionRequest
    {
        public int ProgrammingTaskId { get; set; }
        public Guid UserId { get; set; }

        public string ProgrammingTaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
    public class UpdateSubscriptionRequestProfile : Profile
    {
        public UpdateSubscriptionRequestProfile()
        {
            CreateMap<UpdateSubscriptionRequest, UpdateSubscriptionModel>();
        }
    }
}
