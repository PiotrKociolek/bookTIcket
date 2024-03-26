using BookTicket.Model;
using BookTicket.Model.Dtos.Screening;

namespace BookTicket.Services.Interfaces;

public interface IScreeningService
{
    Task<Screening> AddScreeningAsync(AddScreeningDto dto);
    Task<Screening> EditScreeningAsync(EditScreeningDto dto);
    Task DeleteScreeningAsync(DeleteScreeningDto dto);
    Task<Screening> GetScreeningByIdAsync(int id);
    Task<Screening> GetScreeningDetailsAsync(int id);
    Task<List<Screening>> GetAllScreeningsAsync();
}