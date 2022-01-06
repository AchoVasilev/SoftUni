using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class UserJsonImportModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]{1}[a-z]{2,} [A-Z]{1}[a-z]{2,}")]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(3, 103)]
        public int Age { get; set; }

        public CardJsonImportModel[] Cards { get; set; }
    }
}
