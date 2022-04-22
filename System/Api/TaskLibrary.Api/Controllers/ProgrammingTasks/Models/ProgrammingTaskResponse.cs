using AutoMapper;
using TaskLibrary.ProgrammingTaskService.Models;

namespace TaskLibrary.Api.Controllers.ProgrammingTasks.Models
{
    public class ProgrammingTaskResponse
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

    public class ProgrammingTaskResponseProfile : Profile
    {
        public ProgrammingTaskResponseProfile()
        {
            CreateMap<ProgrammingTaskModel, ProgrammingTaskResponse>();
        }
    }
}
