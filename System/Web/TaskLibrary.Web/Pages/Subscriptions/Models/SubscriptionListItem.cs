namespace TaskLibrary.Web.Pages.Subscriptions.Models
{
    public class SubscriptionListItem
    {
        public int Id { get; set; }

        public int ProgrammingTaskId { get; set; }
        public Guid UserId { get; set; }

        public string ProgrammingTaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
    }
}
