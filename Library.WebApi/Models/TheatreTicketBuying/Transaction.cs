namespace Library.WebApi.Models.TheatreTicketBuying
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public decimal Price { get; set; }
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public List<Buyer> Buyers { get; set; }

    }
}
