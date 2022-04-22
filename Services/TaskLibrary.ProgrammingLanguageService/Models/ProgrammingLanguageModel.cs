using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Db.Entities;

namespace TaskLibrary.ProgrammingLanguageService.Models
{
    public class ProgrammingLanguageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Level { get; set; }

        public string Paradigm { get; set; }

        public string Realization { get; set; }
    }
    public class ProgrammingLanguageModelProfile : Profile
    {
        public ProgrammingLanguageModelProfile()
        {
            CreateMap<ProgrammingLanguage, ProgrammingLanguageModel>();
        }
    }
}
