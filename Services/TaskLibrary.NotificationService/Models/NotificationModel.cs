using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.NotificationService.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }

        public string CommentAuthor { get; set; }
        public string TaskName { get; set; }
        public int? SubscribtionId { get; set; }
        public string Text { get; set; }
    }
    public class NotificationModelProfile : Profile
    {
        public NotificationModelProfile()
        {
            CreateMap<Notification, NotificationModel>()
                .ForMember(dest => dest.CommentAuthor, opt => opt.MapFrom(src => (src.Subscription!=null)?src.Subscription.User.FullName:"[данные удалены]"))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => (src.Subscription != null)?src.Subscription.ProgrammingTask.Name: "[данные удалены]"));
        }
    }
}
