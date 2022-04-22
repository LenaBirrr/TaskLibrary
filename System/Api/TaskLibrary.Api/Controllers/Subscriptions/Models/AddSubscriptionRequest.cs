using AutoMapper;
using TaskLibrary.SubscriptionService.Models;

namespace TaskLibrary.Api.Controllers.Subscriptions.Models
{
    public class AddSubscriptionRequest
    {
        public int ProgrammingTaskId { get; set; }
        public Guid UserId { get; set; }

        public string ProgrammingTaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
    public class AddSubscriptionRequestProfile : Profile
    {
        public AddSubscriptionRequestProfile()
        {
            CreateMap<AddSubscriptionRequest, AddSubscriptionModel>();
        }
    }
}
