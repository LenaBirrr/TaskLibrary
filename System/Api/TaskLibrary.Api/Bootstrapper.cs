using TaskLibrary.CategoryService;
using TaskLibrary.CommentService;
using TaskLibrary.NotificationService;
using TaskLibrary.ProgrammingLanguageService;
using TaskLibrary.ProgrammingTaskService;
using TaskLibrary.Settings;
using TaskLibrary.SolutionService;
using TaskLibrary.SubscriptionService;

namespace TaskLibrary.Api
{
    public static class Bootstrapper
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services
                .AddSettings()
                .AddCategoryService()
                .AddCommentService()
                .AddNotificationService()
                .AddProgrammingLanguageService()
                .AddProgrammingTaskService()
                .AddSolutionService()
                .AddSubscriptionService()
                //.AddEmailSender()
                //.AddRabbitMq()
                //.AddUserAccountService()
                ;
        }
    }
}
