using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.Common.Exceptions;
using TaskLibrary.Common.Validator;
using TaskLibrary.Db.Context.Context;
using TaskLibrary.Db.Entities;
using TaskLibrary.NotificationService.Models;

namespace TaskLibrary.NotificationService
{
    public class NotificationService:INotificationService
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<AddNotificationModel> addNotificationModelValidator;

        public NotificationService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddNotificationModel> addNotificationModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addNotificationModelValidator = addNotificationModelValidator;
        }

        public async Task<IEnumerable<NotificationModel>> GetNotifications()
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var notifications = context
                .Notifications
                .AsQueryable();

            notifications = notifications
                .Take(1000);

            var data = (await notifications.ToListAsync()).Select(notification => mapper.Map<NotificationModel>(notification));

            return data;

        }
        public async Task<NotificationModel> GetNotification(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var notification = await context.Notifications.FirstOrDefaultAsync(x => x.Id.Equals(id));

            var data = mapper.Map<NotificationModel>(notification);

            return data;
        }
        public async Task<NotificationModel> AddNotification(AddNotificationModel model)
        {
            addNotificationModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var notification = mapper.Map<Notification>(model);
            await context.Notifications.AddAsync(notification);
            context.SaveChanges();

            return mapper.Map<NotificationModel>(notification);
        }
        
        public async Task DeleteNotification(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var notification = await context.Notifications.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The notification (id: {id}) was not found");

            context.Remove(notification);
            context.SaveChanges();
        }
    }
}
