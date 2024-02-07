using ConversorDeMonedasBack.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConversorDeMonedasBack.Models.Dtos
{
    public class CreateAndUpdateCurrencyDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public decimal Value { get; set; }
    }
}
