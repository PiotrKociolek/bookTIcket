using BookTicket.Model.Flag;

namespace BookTicket.Model.Dtos.Screening;

public class AddScreeningDto
{
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public DateTime Time { get; set; }
    public int RoomCapacity { get; set; }
    public ScreeningRoom Room { get; set; }
}
