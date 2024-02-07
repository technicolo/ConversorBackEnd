using ConversorDeMonedasBack.Entities;
using ConversorDeMonedasBack.Models.Enum;

namespace ConversorDeMonedasBack.Models.Dtos
{
    public class GetCurrencyByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Value { get; set; }
    }
}
