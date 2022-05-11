using System.Text;
using System.Text.Json;
using TaskLibrary.Web.Pages.Languages.Models;

namespace TaskLibrary.Web.Pages.Languages.Services
{
    public class LanguageService:ILanguageService
    {
        private readonly HttpClient _httpClient;

        public LanguageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<LanguageListItem>> GetLanguages(int offset = 0, int limit = 10)
        {
            string url = $"{Settings.ApiRoot}/v1/languages";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<LanguageListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<LanguageListItem>();

            return data;
        }

        public async Task<LanguageListItem> GetLanguage(int langId)
        {
            string url = $"{Settings.ApiRoot}/v1/languages/{langId}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<LanguageListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LanguageListItem();

            return data;
        }

        public async Task AddLanguage(LanguageModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/languages";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task EditLanguage(int langId, LanguageModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/languages/{langId}";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task DeleteLanguage(int langId)
        {
            string url = $"{Settings.ApiRoot}/v1/languages/{langId}";

            var response = await _httpClient.DeleteAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

    }
}

