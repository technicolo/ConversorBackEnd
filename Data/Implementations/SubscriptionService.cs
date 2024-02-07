using ConversorDeMonedasBack.Data.Interfaces;
using ConversorDeMonedasBack.Entities;

namespace ConversorDeMonedasBack.Data.Implementations
{
    public class SubscriptionService : ISubscriptionService
    {   
        private readonly CurrencyConverterContext _context;
        public SubscriptionService(CurrencyConverterContext context)
        {
            _context = context;
        }

        public List<Subscription> GetAllSubscriptions()
        {
            return _context.Subscriptions.Where(s => s.Id != 10).ToList();
        }
        public Subscription? GetSubscriptionById(int subscriptionId)
        {
            return _context.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
        }

        public int GetSubscriptionAmountOfConversions(int subscriptionId)
        {
            Subscription? subscription = GetSubscriptionById(subscriptionId);
            if (subscription != null)
            {
                return subscription.AmountOfConversions;
            }
            return 0;
        }

    }
}
