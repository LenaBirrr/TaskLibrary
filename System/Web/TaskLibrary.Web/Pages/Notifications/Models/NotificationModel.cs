
using FluentValidation;

namespace TaskLibrary.Web.Pages.Notifications.Models
{
    public class NotificationModel
    {
        public int? Id { get; set; }
        public string TaskName { get; set; }
        public int? SubscribtionId { get; set; }
        public string Text { get; set; }
    }
    public class NotificationModelValidator : AbstractValidator<NotificationModel>
    {       
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<NotificationModel>.CreateWithOptions((NotificationModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }

    
}
