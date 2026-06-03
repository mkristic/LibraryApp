namespace Library.WebApi.Models.TheatreTicketBuying
{
    public class Play
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
        public int PerformanceId { get; set; }

        public Performance Performance { get; set; }    
    }


}
