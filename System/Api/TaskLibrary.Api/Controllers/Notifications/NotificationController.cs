using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Api.Controllers.Notifications.Models;
using TaskLibrary.NotificationService;

namespace TaskLibrary.Api.Controllers.Notifications
{
    [Route("api/v{version:apiVersion}/notifications")]
    [ApiController]
    [ApiVersion("1.0")]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<NotificationController> logger;
        private readonly INotificationService notificationService;

        public NotificationController(IMapper mapper, ILogger<NotificationController> logger, INotificationService notificationService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.notificationService = notificationService;
        }

        [HttpGet("")]
        // [Authorize(AppScopes.BooksRead)]
        public async Task<IEnumerable<NotificationResponse>> GetNotifications()
        {
            var notifications = await notificationService.GetNotifications();
            var response = mapper.Map<IEnumerable<NotificationResponse>>(notifications);

            return response;
        }

        [HttpGet("{id}")]
        //[Authorize(AppScopes.BooksRead)]
        public async Task<NotificationResponse> GetNotificationById([FromRoute] int id)
        {
            var notification = await notificationService.GetNotification(id);
            var response = mapper.Map<NotificationResponse>(notification);

            return response;
        }
    }

}
