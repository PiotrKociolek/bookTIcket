namespace BookTicket.Model;

public class Screening
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public DateTime DateAndTime { get; set; }
    public int ScreeningRoom { get; set; }
    public int TotalTickets { get; set; }
    public int BookedTickets { get; set; }
}