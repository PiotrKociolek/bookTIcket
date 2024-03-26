using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Model.Dtos.Ticket;

namespace BookTicket.Services.Interfaces;

public interface ITicketService
{
    Task<Ticket> AddTicketAsync(TicketDto dto);
    void DeleteTicketById(int id);
    Task<Ticket> GetTicketDataAsync(int id);
    Task<List<Ticket>> GetTicketsByUserAsync(int userId);
}