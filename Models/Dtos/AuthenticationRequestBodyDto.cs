using System.ComponentModel.DataAnnotations;

namespace ConversorDeMonedasBack.Models.Dtos
{
    public class AuthenticationRequestBodyDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
