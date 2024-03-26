using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Model.Dtos.Ticket;
using BookTicket.Model.Flag;
using BookTicket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.Services.Implementation;

public class TicketService : ITicketService

{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Ticket> AddTicketAsync(TicketDto dto)
    {
        var screening = await _context.Screenings
            .Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.Id == dto.ScreeningId);

        if (screening == null)
        {
            throw new ArgumentException("Screening with the provided ID was not found.");
        }

        var newTicket = new Ticket
        {
            UserId = dto.UserId,
            ScreeningId = dto.ScreeningId,
            Screening = screening,
            Room = screening.ScreeningRoom,
        };

        _context.Tickets.Add(newTicket);
        await _context.SaveChangesAsync();
        return newTicket;
    }


    public void DeleteTicketById(int id)
    {
        var ticketToDelete = _context.Tickets.Find(id);
        if (ticketToDelete != null)
        {
            _context.Tickets.Remove(ticketToDelete);
            _context.SaveChanges(); 
        }
    }


    public async Task<Ticket> GetTicketDataAsync(int id)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Screening)
            .ThenInclude(s => s.Movie)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (ticket.Screening == null || ticket.Screening.Movie == null)
        {
            throw new Exception("Invalid Data.");
        }

        return ticket;
    }

    public async Task<List<Ticket>> GetTicketsByUserAsync(int userId)
    {
        var tickets = await _context.Tickets
            .Include(t => t.Screening)
            .ThenInclude(s => s.Movie)
            .Where(t => t.UserId == userId)
            .ToListAsync();
        return tickets;
    }


    private TicketStatus TicketStatusBooked(Ticket ticket)
    {
        return ticket.Status = TicketStatus.Booked;
    }
}