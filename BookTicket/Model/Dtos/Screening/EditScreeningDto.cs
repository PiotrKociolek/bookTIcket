using BookTicket.Model.Flag;

namespace BookTicket.Model.Dtos.Screening;

public class EditScreeningDto
{
    public int ScreeningId { get; set; }
    public string MovieTitle { get; set; }
    public DateTime Time { get; set; }
    public ScreeningRoom ScreeningRoom  { get; set; }
    public int RoomCapacity { get; set; }
}
