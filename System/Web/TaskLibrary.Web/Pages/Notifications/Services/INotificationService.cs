using TaskLibrary.Web.Pages.Notifications.Models;

namespace TaskLibrary.Web.Pages.Notifications.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationListItem>> GetNotifications(int offset = 0, int limit = 10);
        Task<NotificationListItem> GetNotification(int bookId);
    }
}
