﻿using System;
using System.ComponentModel.DataAnnotations;

using static CarShop.Data.DataConstants;

namespace CarShop.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [MaxLength(UsernameMaxLength)]
        [Required]
        public string Username { get; init; }

        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }

        public bool IsMechanic { get; init; }
    }
}
