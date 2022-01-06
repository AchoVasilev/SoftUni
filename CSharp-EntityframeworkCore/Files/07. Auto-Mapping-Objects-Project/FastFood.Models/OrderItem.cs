using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class OrderItem
	{
		public int OrderId { get; set; }

		[Required]
		public virtual Order Order { get; set; }

		public int ItemId { get; set; }

		[Required]
        public virtual Item Item { get; set; }

		[Range(1, int.MaxValue)]
		public int Quantity { get; set; }
	}
}