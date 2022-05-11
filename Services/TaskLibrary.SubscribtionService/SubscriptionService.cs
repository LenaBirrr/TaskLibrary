using AutoMapper;
using Microsoft.EntityFrameworkCore;

using TaskLibrary.Common.Exceptions;
using TaskLibrary.Common.Validator;
using TaskLibrary.Db.Context.Context;
using TaskLibrary.Db.Entities;

using TaskLibrary.SubscriptionService.Models;

namespace TaskLibrary.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IMapper mapper;
        private readonly IModelValidator<AddSubscriptionModel> addSubscriptionModelValidator;
        private readonly IModelValidator<UpdateSubscriptionModel> updateSubscriptionModelValidator;

        public SubscriptionService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddSubscriptionModel> addSubscriptionModelValidator,
        IModelValidator<UpdateSubscriptionModel> updateSubscriptionModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addSubscriptionModelValidator = addSubscriptionModelValidator;
            this.updateSubscriptionModelValidator = updateSubscriptionModelValidator;
        }

        public async Task<IEnumerable<SubscriptionModel>> GetSubscriptions(int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var subscriptions = context
                .Subscriptions.Include(x => x.User).Include(x => x.ProgrammingTask)
                .AsQueryable();

            subscriptions = subscriptions
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await subscriptions.ToListAsync()).Select(subscription => mapper.Map<SubscriptionModel>(subscription));

            return data;

        }
        public async Task<IEnumerable<SubscriptionModel>> GetSubscriptionsByUser(Guid userId, int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var subscriptions = context
                .Subscriptions.Include(x => x.User).Include(x => x.ProgrammingTask)
                .Where(x => x.UserId.Equals(userId))
                .AsQueryable();

            subscriptions = subscriptions
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await subscriptions.ToListAsync()).Select(subscription => mapper.Map<SubscriptionModel>(subscription));

            return data;
        }

        public async Task<SubscriptionModel> GetSubscription(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var subscriptions = await context.Subscriptions.Include(x => x.User).Include(x => x.ProgrammingTask).FirstOrDefaultAsync(x => x.Id.Equals(id));

            var data = mapper.Map<SubscriptionModel>(subscriptions);

            return data;
        }
        public async Task<SubscriptionModel> AddSubscription(AddSubscriptionModel model)
        {
            addSubscriptionModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var subscription = mapper.Map<Subscription>(model);
            await context.Subscriptions.AddAsync(subscription);
            context.SaveChanges();

            return mapper.Map<SubscriptionModel>(subscription);
        }
        public async Task UpdateSubscription(int id, UpdateSubscriptionModel model)
        {
            updateSubscriptionModelValidator.Check(model);

            using var context = await contextFactory.CreateDbContextAsync();

            var subscription = await context.Subscriptions.FirstOrDefaultAsync(x => x.Id.Equals(id));

            ProcessException.ThrowIf(() => subscription is null, $"The subscription (id: {id}) was not found");

            subscription = mapper.Map(model, subscription);

            context.Subscriptions.Update(subscription);
            context.SaveChanges();
        }
        public async Task DeleteSubscription(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var subscription = await context.Subscriptions.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The solution (id: {id}) was not found");

            context.Remove(subscription);
            context.SaveChanges();
        }

        public async Task<IEnumerable<SubscriptionModel>> GetSubscriptionsByTask(int taskId, int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var subscriptions = context
                .Subscriptions.Include(x => x.User).Include(x => x.ProgrammingTask)
                .Where(x => x.ProgrammingTaskId.Equals(taskId))
                .AsQueryable();

            subscriptions = subscriptions
                .Skip(Math.Max(offset, 0))
                .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await subscriptions.ToListAsync()).Select(subscription => mapper.Map<SubscriptionModel>(subscription));

            return data;
        }
    }
}
