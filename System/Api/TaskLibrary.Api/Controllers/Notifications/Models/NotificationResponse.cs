using AutoMapper;
using TaskLibrary.NotificationService.Models;

namespace TaskLibrary.Api.Controllers.Notifications.Models
{
    public class NotificationResponse
    {
        public int Id { get; set; }

        public string TaskName { get; set; }
        public int? SubscribtionId { get; set; }
        public string Text { get; set; }
    }

    public class NotificationResponseProfile : Profile
    {
        public NotificationResponseProfile()
        {
            CreateMap<NotificationModel, NotificationResponse>();
        }
    }
}
