using AutoMapper;
using TaskLibrary.SubscriptionService.Models;

namespace TaskLibrary.Api.Controllers.Subscriptions.Models
{
    public class SubscriptionResponse
    {
        public int Id { get; set; }

        public int ProgrammingTaskId { get; set; }
        public Guid UserId { get; set; }

        public string ProgrammingTaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
    public class SubscriptionResponseProfile : Profile
    {
        public SubscriptionResponseProfile()
        {
            CreateMap<SubscriptionModel, SubscriptionResponse>();
        }
    }
}
