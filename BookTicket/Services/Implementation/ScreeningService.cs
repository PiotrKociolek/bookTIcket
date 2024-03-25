using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Model.Dtos.request;
using BookTicket.Model.Dtos.Screening;
using BookTicket.Services.Interfaces;

namespace BookTicket.Services.Implementation;

public class ScreeningService : IScreeningService
{
    private readonly AppDbContext _context;

    public ScreeningService(AppDbContext context)
    {
        _context = context;
    }

    public void AddScreening(AddScreeningDto dto)
    {
        var newScreening = new Screening
        {
            MovieId = dto.MovieId,
            DateAndTime = dto.Time,
            ScreeningRoom = dto.Room, 
            TotalTickets = dto.RoomCapacity,
            BookedTickets = 0 
        };
        _context.Screenings.Add(newScreening);
        _context.SaveChanges();

    }


    public void EditScreening(EditScreeningDto dto)
    {
        var editScreening = _context.Screenings.FirstOrDefault(s => s.Id == dto.ScreeningId);
        editScreening.MovieId = dto.ScreeningId;
        editScreening.DateAndTime = dto.Time;
        editScreening.ScreeningRoom = dto.ScreeningRoom;
        editScreening.TotalTickets = dto.RoomCapacity;

        _context.SaveChanges();
    }

    public void DeleteScreening(DeleteScreeningDto dto)
    {
        var screeningToDelete = _context.Screenings.FirstOrDefault(s => s.Id == dto.ScreeningId);
        _context.Screenings.Remove(screeningToDelete);
        _context.SaveChanges(); 
    }
}
    