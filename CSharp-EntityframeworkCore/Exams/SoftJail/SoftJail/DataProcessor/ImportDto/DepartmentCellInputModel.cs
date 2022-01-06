using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class DepartmentCellInputModel
    {
        public DepartmentCellInputModel()
        {
            Cells = new HashSet<CellInputModel>();
        }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        public ICollection<CellInputModel> Cells { get; set; }
    }
}
