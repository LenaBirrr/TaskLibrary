using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskLibrary.Api.Controllers.Notifications.Models;
using TaskLibrary.Common.Security;
using TaskLibrary.NotificationService;

namespace TaskLibrary.Api.Controllers.Notifications
{
    [Route("api/v{version:apiVersion}/notifications")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
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
        [Authorize(AppScopes.NotificationsRead)]
        public async Task<IEnumerable<NotificationResponse>> GetNotificationsByUser([FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            var id = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var notifications = await notificationService.GetNotificationsByUser(id, offset, limit);
            var response = mapper.Map<IEnumerable<NotificationResponse>>(notifications);

            return response;
        }

        [HttpGet("{id}")]
        [Authorize(AppScopes.NotificationsRead)]
        public async Task<NotificationResponse> GetNotificationById([FromRoute] int id)
        {
            var notification = await notificationService.GetNotification(id);
            var response = mapper.Map<NotificationResponse>(notification);

            return response;
        }
    }

}
