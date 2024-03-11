using Template.Modules.Core.Application.Messages.Queries.Tauron;
using Template.Modules.Integrations.Tauron.Application.Dto;
using Template.Modules.Integrations.Tauron.Application.Services;
using MediatR;

namespace Template.Modules.Core.Application.Handlers.Queries.Tauron
{
    public class GetOutagesByStreetHandler : IRequestHandler<GetOutagesByStreet, ICollection<OutageDto>>
    {
        private readonly ITauronService _tauronService;

        public GetOutagesByStreetHandler(ITauronService tauronService)
        {
            _tauronService = tauronService;
        }

        public async Task<ICollection<OutageDto>> Handle(GetOutagesByStreet request, CancellationToken cancellationToken)
        {
            return await _tauronService.GetOutageItemsAsync(request.StreetName, request.HouseNumber, request.From, request.To, cancellationToken);
        }
    }
}
