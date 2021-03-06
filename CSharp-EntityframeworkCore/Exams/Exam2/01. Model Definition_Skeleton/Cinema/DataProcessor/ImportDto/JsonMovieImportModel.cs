using Cinema.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.DataProcessor.ImportDto
{
    public class JsonMovieImportModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public Genre? Genre { get; set; }

        [Required]
        public TimeSpan? Duration { get; set; }

        [Range(1, 10)]
        public double Rating { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Director { get; set; }
    }
}
