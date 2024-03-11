using Template.Modules.Core.Application.Dto;
using MediatR;

namespace Template.Modules.Core.Application.Messages.Queries
{
    public class GetImage : IRequest<ImageDto>
    {
        public string Id { get; set; }
    }
}
