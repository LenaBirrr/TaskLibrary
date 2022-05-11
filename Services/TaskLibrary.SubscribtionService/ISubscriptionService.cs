using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskLibrary.SubscriptionService.Models;

namespace TaskLibrary.SubscriptionService
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionModel>> GetSubscriptions(int offset = 0, int limit = 10);

        Task<IEnumerable<SubscriptionModel>> GetSubscriptionsByUser(Guid userId, int offset = 0, int limit = 10);
        Task<IEnumerable<SubscriptionModel>> GetSubscriptionsByTask(int taskId, int offset = 0, int limit = 10);

        Task<SubscriptionModel> GetSubscription(int id);
        Task<SubscriptionModel> AddSubscription(AddSubscriptionModel model);
        Task UpdateSubscription(int id, UpdateSubscriptionModel model);
        Task DeleteSubscription(int id);
    }
}
