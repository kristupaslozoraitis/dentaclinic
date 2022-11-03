﻿using System.ComponentModel.DataAnnotations;

namespace DentaClinic.Models.Dtos
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}