using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.CommentService.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public string UserName { get; set; }

        public string TaskName { get; set; }
        public int ProgrammingTaskId { get; set; }

        public Guid UserId { get; set; }
    }
    public class CommentModelProfile : Profile
    {
        public CommentModelProfile()
        {
            CreateMap<Comment, CommentModel>()
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.ProgrammingTask.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName));
        }
    }
}
