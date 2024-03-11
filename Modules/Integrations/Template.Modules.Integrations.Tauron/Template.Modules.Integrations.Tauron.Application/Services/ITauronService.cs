using Template.Modules.Integrations.Tauron.Application.Dto;

namespace Template.Modules.Integrations.Tauron.Application.Services
{
    public interface ITauronService
    {
        Task<ICollection<OutageDto>> GetOutageItemsAsync(string streetName, int houseNumber, DateTime from, DateTime to, CancellationToken cancellationToken);
    }
}
