using BookTicket.Model;

namespace BookTicket.Data;

public class Ticket
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ScreeningId { get; set; }
    public DateTime BookingTime { get; set; }
}