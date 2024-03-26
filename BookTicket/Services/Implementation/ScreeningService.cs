using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Model.Dtos.Screening;
using BookTicket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.Services.Implementation;

public class ScreeningService : IScreeningService
{
    private readonly AppDbContext _context;

    public ScreeningService(AppDbContext context)
    {
        _context = context;
    }

 
    public async Task AddScreeningAsync(AddScreeningDto dto)
    {
        var newScreening = new Screening
        {
            MovieTitle = dto.MovieTitle,
            DateAndTime = dto.Time,
            ScreeningRoom = dto.Room,
            TotalTickets = dto.RoomCapacity,
            BookedTickets = 0
        };

        _context.Screenings.Add(newScreening);
        await _context.SaveChangesAsync();
    }

    public async Task EditScreeningAsync(EditScreeningDto dto)
    {
        var editScreening = await _context.Screenings.FirstOrDefaultAsync(s => s.Id == dto.ScreeningId);
        if (editScreening != null)
        {
            editScreening.MovieTitle = dto.MovieTitle;
            editScreening.DateAndTime = dto.Time;
            editScreening.ScreeningRoom = dto.ScreeningRoom;
            editScreening.TotalTickets = dto.RoomCapacity;

            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("No screening with selected id");
        }
    }

    public async Task DeleteScreeningAsync(DeleteScreeningDto dto)
    {
        var screeningToDelete = await _context.Screenings.FirstOrDefaultAsync(s => s.Id == dto.ScreeningId);
        if (screeningToDelete != null)
        {
            _context.Screenings.Remove(screeningToDelete);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("No screening with selected id");
        }
    }
}

    