using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.NotificationService.Models;

namespace TaskLibrary.NotificationService
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationModel>> GetNotifications();
        Task<NotificationModel> GetNotification(int id);
        Task<NotificationModel> AddNotification(AddNotificationModel model);
        Task DeleteNotification(int id);
    }
}
