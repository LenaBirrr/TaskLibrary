
using TaskLibrary.ProgrammingLanguageService.Models;

namespace TaskLibrary.ProgrammingLanguageService
{
    public interface IProgrammingLanguageService
    {
        Task<IEnumerable<ProgrammingLanguageModel>> GetProgrammingLanguages(int offset = 0, int limit = 10);
        Task<ProgrammingLanguageModel> GetProgrammingLanguage(int id);
        Task<ProgrammingLanguageModel> AddProgrammingLanguage(AddProgrammingLanguageModel model);
        Task UpdateProgrammingLanguage(int id, UpdateProgrammingLanguageModel model);
        Task DeleteProgrammingLanguage(int id);
    }
}
