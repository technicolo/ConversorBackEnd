using ConversorDeMonedasBack.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConversorDeMonedasBack.Data.Interfaces
{
    public interface ISubscriptionService
    {
        List<Subscription> GetAllSubscriptions();
        Subscription? GetSubscriptionById(int subscriptionId);
        int GetSubscriptionAmountOfConversions(int subscriptionId);
    }
}
