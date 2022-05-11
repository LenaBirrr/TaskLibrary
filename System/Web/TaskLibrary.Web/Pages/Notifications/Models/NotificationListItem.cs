namespace TaskLibrary.Web.Pages.Notifications.Models
{
    public class NotificationListItem
    {
        public int Id { get; set; }

        public string TaskName { get; set; }
        public int? SubscribtionId { get; set; }
        public string Text { get; set; }
    }
}
