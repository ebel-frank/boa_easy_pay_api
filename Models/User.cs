﻿using System.ComponentModel.DataAnnotations;

namespace BoaEasyPay.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public required string Username { get; set; }

        [EmailAddress]
        [Required]
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
