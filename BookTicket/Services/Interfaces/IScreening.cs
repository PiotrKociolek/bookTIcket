using BookTicket.Model;
using BookTicket.Model.Dtos.Screening;

namespace BookTicket.Services.Interfaces;

public interface IScreeningService
{
    void AddScreening(AddScreeningDto dto);
    void EditScreening(EditScreeningDto dto);
    void DeleteScreening(DeleteScreeningDto dto);
}
