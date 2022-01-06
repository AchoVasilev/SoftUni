using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

	    public int Id { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 3)]
	    public string Name { get; set; }

	    public virtual ICollection<Item> Items { get; set; }
    }
}
