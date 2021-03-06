using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Employee
	{
        public Employee()
        {
			Orders = new HashSet<Order>();
        }

		public int Id { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 3)]
		public string Name { get; set; }

		[Required]
		[Range(15, 80)]
		public int Age { get; set; }

	    [Required]
	    [StringLength(30, MinimumLength = 3)]
	    public string Address { get; set; }

        public int PositionId { get; set; }

		[Required]
		public virtual Position Position { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
	}
}