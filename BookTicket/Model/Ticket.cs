using BookTicket.Model.Flag;

namespace BookTicket.Model;

public class Ticket
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ScreeningId { get; set; }
    public DateTime ScreeningTime { get; set; }
    public Screening Screening { get; set; }
    public ScreeningRoom Room { get; set; }
    public TicketStatus Status { get; set; }
}