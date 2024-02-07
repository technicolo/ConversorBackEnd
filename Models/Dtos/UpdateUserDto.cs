using System.ComponentModel.DataAnnotations;

namespace ConversorDeMonedasBack.Models.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public int SubscriptionId { get; set; }
    }
}
