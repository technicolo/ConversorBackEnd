﻿using System.ComponentModel.DataAnnotations;

namespace ConversorDeMonedasBack.Models.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
