using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConversorDeMonedasBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public IActionResult GetAllSubscriptions()
        {
            return Ok(_subscriptionService.GetAllSubscriptions());
        }
        
        [HttpGet("{subscriptionId}")]
        public IActionResult GetSubscriptionById(int subscriptionId)
        {

            Subscription subscription = _subscriptionService.GetSubscriptionById(subscriptionId);
            if (subscription is null)
            {
                return NotFound();
            }

            return Ok(subscription);
        }

        [HttpGet("{subscriptionId}/amountOfConversions")]
        public IActionResult GetSubscriptionAmountOfConversions(int subscriptionId)
        {
            int amountOfConversions = _subscriptionService.GetSubscriptionAmountOfConversions(subscriptionId);
            return Ok(amountOfConversions);
        }


    }
}
