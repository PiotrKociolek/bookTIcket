using BookTicket.Model.Flag;

namespace BookTicket.Model;

public class Screening
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
    public DateTime DateAndTime { get; set; }
    public ScreeningRoom ScreeningRoom { get; set; }
    public int TotalTickets { get; set; }
    public int BookedTickets { get; set; }
}