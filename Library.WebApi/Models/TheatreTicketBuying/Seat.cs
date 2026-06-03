namespace Library.WebApi.Models.TheatreTicketBuying
{
    public class Seat
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
