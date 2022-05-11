using TaskLibrary.Web.Pages.Languages.Models;

namespace TaskLibrary.Web.Pages.Languages.Services
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguageListItem>> GetLanguages(int offset = 0, int limit = 10);
        Task<LanguageListItem> GetLanguage(int bookId);
        Task AddLanguage(LanguageModel model);
        Task EditLanguage(int bookId, LanguageModel model);
        Task DeleteLanguage(int bookId);
    }
}
