using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class OfficerPrisoner
    {
        [ForeignKey(nameof(Officer))]
        public int OfficerId { get; set; }

        public virtual Officer Officer { get; set; }

        [ForeignKey(nameof(Prisoner))]
        public int PrisonerId { get; set; }

        public virtual Prisoner Prisoner { get; set; }
    }
}
