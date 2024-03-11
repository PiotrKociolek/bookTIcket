using Template.Modules.Integrations.Tauron.Application.Dto;
using MediatR;
namespace Template.Modules.Core.Application.Messages.Queries.Tauron
{
    public record GetOutagesByStreet(string StreetName, int HouseNumber, DateTime From, DateTime To) : IRequest<ICollection<OutageDto>>;
}
