using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TaskLibrary.Web.Pages.Categories.Models;

namespace TaskLibrary.Web.Pages.Categories.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryListItem>> GetCategories(int offset = 0, int limit = 10)
        {
            string url = $"{Settings.ApiRoot}/v1/categories";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<CategoryListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CategoryListItem>();

            return data;
        }

        public async Task<CategoryListItem> GetCategory(int categoryId)
        {
            string url = $"{Settings.ApiRoot}/v1/categories/{categoryId}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<CategoryListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new CategoryListItem();

            return data;
        }

        public async Task AddCategory(CategoryModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/categories";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task EditCategory(int categoryId, CategoryModel model)
        {
            string url = $"{Settings.ApiRoot}/v1/categories/{categoryId}";

            var body = JsonSerializer.Serialize(model);
            var request = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

        public async Task DeleteCategory(int categoryId)
        {
            string url = $"{Settings.ApiRoot}/v1/categories/{categoryId}";

            var response = await _httpClient.DeleteAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }

    }
}
