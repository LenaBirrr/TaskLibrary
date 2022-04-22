using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.SolutionService.Models
{
    public class SolutionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ProgrammingTaskId { get; set; }

        public string AuthorName { get; set; }

        public Guid? UserId { get; set; }
        public string ProgrammingTaskName { get; set; }

        public string ProgrammingLanguageName { get; set; }


        public int? ProgrammingLanguageId { get; set; }
    }
    public class SolutionModelProfile : Profile
    {
        public SolutionModelProfile()
        {
            CreateMap<Solution, SolutionModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => (src.User != null ? src.User.FullName : "Неизвестный автор")))
                .ForMember(dest => dest.ProgrammingTaskName, opt => opt.MapFrom(src => src.ProgrammingTask.Name))
                .ForMember(dest => dest.ProgrammingLanguageName, opt => opt.MapFrom(src => src.ProgrammingLanguage != null ? src.ProgrammingLanguage.Name : "Язык не указан"));

        }
    }
}
