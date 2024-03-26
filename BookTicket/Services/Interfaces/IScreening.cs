using BookTicket.Model;
using BookTicket.Model.Dtos.Screening;

namespace BookTicket.Services.Interfaces;

public interface IScreeningService
{
    Task AddScreeningAsync(AddScreeningDto dto);
    Task EditScreeningAsync(EditScreeningDto dto);
    Task DeleteScreeningAsync(DeleteScreeningDto dto);
}