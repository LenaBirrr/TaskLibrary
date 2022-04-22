using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.SubscriptionService.Models
{
    public class SubscriptionModel
    {
        public int Id { get; set; }

        public int ProgrammingTaskId { get; set; }
        public Guid UserId { get; set; }

        public string ProgrammingTaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }

    public class SubscriptionModelProfile : Profile
    {
        public SubscriptionModelProfile()
        {
            CreateMap<Subscription, SubscriptionModel>()
                .ForMember(dest => dest.ProgrammingTaskName, opt => opt.MapFrom(src => src.ProgrammingTask.Name));

        }
    }
}
