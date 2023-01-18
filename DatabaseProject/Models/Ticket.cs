namespace Championship_Login_API.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Guid IdMatch { get; set; }
        public Guid IdUser { get; set; }
        public int TicketNumber { get; set; }
    }
}
