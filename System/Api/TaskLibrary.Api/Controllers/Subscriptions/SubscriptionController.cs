using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskLibrary.Api.Controllers.Subscriptions.Models;
using TaskLibrary.SubscriptionService;
using TaskLibrary.SubscriptionService.Models;

namespace TaskLibrary.Api.Controllers.Subscriptions
{
    [Route("api/v{version:apiVersion}/subscription")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SubscriptionController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILogger<SubscriptionController> logger;
        private readonly ISubscriptionService subscriptionService;

        public SubscriptionController(IMapper mapper, ILogger<SubscriptionController> logger, ISubscriptionService subscriptionService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.subscriptionService = subscriptionService;
        }

        [HttpGet("")]
        // [Authorize(AppScopes.BooksRead)]
        public async Task<IEnumerable<SubscriptionResponse>> GetSubscriptions()
        {
            var subscriptions = await subscriptionService.GetSubscriptions();
            var response = mapper.Map<IEnumerable<SubscriptionResponse>>(subscriptions);

            return response;
        }
        [HttpGet("Byuser/{id}")]
        public async Task<IEnumerable<SubscriptionResponse>> GetSubscriptionsByUser([FromRoute] Guid id)
        {
            var subscriptions = await subscriptionService.GetSubscriptionsByUser(id);
            var response = mapper.Map<IEnumerable<SubscriptionResponse>>(subscriptions);

            return response;
        }

        //[HttpGet("Bytask/{id}")]
       /* public async Task<IEnumerable<SubscriptionResponse>> GetSubscriptionsByTask([FromRoute] int id)
        {
            var subscriptions = await subscriptionService.GetSubscriptionsByTask(id);
            var response = mapper.Map<IEnumerable<SubscriptionResponse>>(subscriptions);

            return response;
        }*/


        [HttpGet("{id}")]
        //[Authorize(AppScopes.BooksRead)]
        public async Task<SubscriptionResponse> GetSubscriptionById([FromRoute] int id)
        {
            var subscription = await subscriptionService.GetSubscription(id);
            var response = mapper.Map<SubscriptionResponse>(subscription);

            return response;
        }

        [HttpPost("")]
        //[Authorize(AppScopes.BooksWrite)]
        public async Task<SubscriptionResponse> AddSubscription([FromBody] AddSubscriptionRequest request)
        {
            var model = mapper.Map<AddSubscriptionModel>(request);
            var subscription = await subscriptionService.AddSubscription(model);
            var response = mapper.Map<SubscriptionResponse>(subscription);

            return response;
        }

        [HttpPut("{id}")]
        //[Authorize(AppScopes.BooksWrite)]
        public async Task<IActionResult> UpdateSubscription([FromRoute] int id, [FromBody] UpdateSubscriptionRequest request)
        {
            var model = mapper.Map<UpdateSubscriptionModel>(request);
            await subscriptionService.UpdateSubscription(id, model);

            return Ok();
        }

        [HttpDelete("{id}")]
        //[Authorize(AppScopes.BooksWrite)]
        public async Task<IActionResult> DeleteSubscription([FromRoute] int id)
        {
            await subscriptionService.DeleteSubscription(id);

            return Ok();
        }
    }
}
