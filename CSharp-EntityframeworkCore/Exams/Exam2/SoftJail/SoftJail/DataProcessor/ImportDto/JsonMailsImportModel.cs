using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class JsonMailsImportModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [RegularExpression(@"^([0-9A-z\s]+ str.)$")]
        public string Address { get; set; }
    }
}