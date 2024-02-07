namespace ConversorDeMonedasBack.Models.Dtos
{
    public class GetSubscriptionByIdResponse
    {
        public string Name { get; set; }
        public int AmountOfConversions { get; set; }
        public string Price { get; set; }
    }
}
