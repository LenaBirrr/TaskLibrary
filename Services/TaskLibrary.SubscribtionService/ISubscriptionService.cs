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
        Task<IEnumerable<SubscriptionModel>> GetSubscriptions();

        Task<IEnumerable<SubscriptionModel>> GetSubscriptionsByUser(Guid userId);
        Task<IEnumerable<SubscriptionModel>> GetSubscriptionsByTask(int taskId);

        Task<SubscriptionModel> GetSubscription(int id);
        Task<SubscriptionModel> AddSubscription(AddSubscriptionModel model);
        Task UpdateSubscription(int id, UpdateSubscriptionModel model);
        Task DeleteSubscription(int id);
    }
}
