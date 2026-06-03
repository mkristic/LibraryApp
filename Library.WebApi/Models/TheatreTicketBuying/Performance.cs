namespace Library.WebApi.Models.TheatreTicketBuying
{
    public class Performance
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }
        public int TicketId {  get; set; }  
        public Ticket Ticket {  get; set; }  
        
        public List<Play> plays { get; set; }


    }
}
