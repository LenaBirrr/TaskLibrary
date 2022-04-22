using AutoMapper;
using TaskLibrary.SolutionService.Models;

namespace TaskLibrary.Api.Controllers.Solutions.Models
{
    public class SolutionResponse
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

    public class SolutionResponseProfile : Profile
    {
        public SolutionResponseProfile()
        {
            CreateMap<SolutionModel, SolutionResponse>();

        }
    }
}
