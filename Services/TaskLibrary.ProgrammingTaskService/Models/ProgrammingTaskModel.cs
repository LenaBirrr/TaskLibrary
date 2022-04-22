using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.ProgrammingTaskService.Models
{
    public class ProgrammingTaskModel
    {
        public int Id { get; set; }
        public string ProblemStatement { get; set; }

        public string Name { get; set; }

        public string Answers { get; set; }

        public string AuthorName { get; set; }
        public Guid? UserId { get; set; }

        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }

    }
    public class ProgrammingTaskModelProfile : Profile
    {
        public ProgrammingTaskModelProfile()
        {
            CreateMap<ProgrammingTask, ProgrammingTaskModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => (src.User!=null?src.User.FullName:"Неизвестный автор")))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null?src.Category.Name:"Без категории"));
            
        }
    }

}
