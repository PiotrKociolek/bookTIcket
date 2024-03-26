using BookTicket.Model.Flag;

namespace BookTicket.Model.Dtos.Ticket;

public class TicketDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ScreeningId { get; set; }
}