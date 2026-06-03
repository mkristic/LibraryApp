namespace Library.WebApi.Models.TheatreTicketBuying
{
    public class Ticket
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int SeatId { get; set; }

        public Seat Seat { get; set; }

        public List<Transaction> Transactions { get; set; }
        public List<Performance> Performances { get; set; }

    }
}
