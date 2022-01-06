using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int Age { get; set; }

        public decimal Balance { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
