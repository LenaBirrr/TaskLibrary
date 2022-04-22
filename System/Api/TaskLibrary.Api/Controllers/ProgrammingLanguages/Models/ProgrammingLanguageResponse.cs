using AutoMapper;
using TaskLibrary.ProgrammingLanguageService.Models;

namespace TaskLibrary.Api.Controllers.ProgrammingLanguages.Models
{
    public class ProgrammingLanguageResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Level { get; set; }

        public string Paradigm { get; set; }

        public string Realization { get; set; }
    }
    public class ProgrammingLanguageResponseProfile : Profile
    {
        public ProgrammingLanguageResponseProfile()
        {
            CreateMap<ProgrammingLanguageModel, ProgrammingLanguageResponse>();
        }
    }
}
