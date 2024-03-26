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


    public async Task<Screening> AddScreeningAsync(AddScreeningDto dto)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == dto.MovieId);
        if (movie == null)
        {
            throw new ArgumentException("Movie with the provided id was not found.");
        }

        var newScreening = new Screening
        {
            Movie = movie,
            DateAndTime = dto.Time,
            ScreeningRoom = dto.Room,
            TotalTickets = dto.RoomCapacity,
            BookedTickets = 0
        };

        _context.Screenings.Add(newScreening);
        await _context.SaveChangesAsync();

        return newScreening;
    }


    public async Task<Screening> EditScreeningAsync(EditScreeningDto dto)
    {
        var editScreening = await _context.Screenings.FirstOrDefaultAsync(s => s.Id == dto.ScreeningId);
        if (editScreening != null)
        {
            editScreening.DateAndTime = dto.Time;
            editScreening.ScreeningRoom = dto.ScreeningRoom;
            editScreening.TotalTickets = dto.RoomCapacity;

            var newMovie = await _context.Movies.FindAsync(dto.MovieId);
            if (newMovie != null)
            {
                editScreening.Movie = newMovie;
            }
            else
            {
                throw new ArgumentException("Movie with the provided ID was not found.");
            }

            await _context.SaveChangesAsync();

            return editScreening;
        }
        else
        {
            throw new ArgumentException("No screening with selected id");
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


    public async Task<Screening> GetScreeningByIdAsync(int id)
    {
        var screening = await _context.Screenings
            .Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (screening == null)
        {
            throw new ArgumentException("Wrong id");
        }
        else
        {
            return screening;
        }
    }


    public async Task<Screening> GetScreeningDetailsAsync(int id)
    {
        var screeningDetails = await _context.Screenings
            .Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (screeningDetails == null)
        {
            throw new ArgumentException("Screening with the provided ID was not found.");
        }

        return screeningDetails;
    }


    public async Task<List<Screening>> GetAllScreeningsAsync()
    {
        var screenings = await _context.Screenings
            .Include(s => s.Movie)
            .ToListAsync();
        return screenings;
    }
}