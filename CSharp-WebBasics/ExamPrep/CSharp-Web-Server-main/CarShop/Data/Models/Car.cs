using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static CarShop.Data.DataConstants;

namespace CarShop.Data.Models
{
    public class Car
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CarMaxLength)]
        public string Model { get; init; }

        public int Year { get; init; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        public string PlateNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Issue> issues { get; set; } = new HashSet<Issue>();
    }
}
