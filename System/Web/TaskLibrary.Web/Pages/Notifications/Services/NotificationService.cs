using System.Text.Json;
using TaskLibrary.Web.Pages.Notifications.Models;
using TaskLibrary.Web.Pages.Notifications.Services;

namespace TaskLibrary.Web.Pages.Notifications.Services
{
    public class NotificationService: INotificationService
    {
        private readonly HttpClient _httpClient;

        public NotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<NotificationListItem>> GetNotifications(int offset = 0, int limit = 10)
        {
            string url = $"{Settings.ApiRoot}/v1/notifications";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<IEnumerable<NotificationListItem>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<NotificationListItem>();

            return data;
        }

        public async Task<NotificationListItem> GetNotification(int langId)
        {
            string url = $"{Settings.ApiRoot}/v1/notifications/{langId}";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            var data = JsonSerializer.Deserialize<NotificationListItem>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new NotificationListItem();

            return data;
        }

    }
}
