using System.Transactions;

namespace Library.WebApi.Models.TheatreTicketBuying
{
    public class Buyer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
