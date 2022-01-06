namespace Cinema.Data.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public int ProjectionId { get; set; }

        public virtual Projection Projection { get; set; }
    }
}
