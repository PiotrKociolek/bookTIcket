using Template.Modules.Core.Application.Dto;
using Template.Modules.Core.Application.Messages.Queries;
using Template.Modules.Core.Core.Domain;
using Template.Modules.Shared.Application.Handlers;
using Template.Modules.Shared.Application.Stores;
using Template.Modules.Shared.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Template.Modules.Core.Application.Handlers.Queries
{
    public class GetImageHandler : BaseHandler<GetImage, ImageDto>
    {
        private readonly IImageStore _imageStore;

        public GetImageHandler(
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            IImageStore imageStore) : base(httpContextAccessor, userManager)
        {
            _imageStore = imageStore;
        }

        public override async Task<ImageDto> Handle(GetImage query, CancellationToken cancellationToken)
        {
            var image = await _imageStore.GetAsync(query.Id);
             
            if (image.content is null)
            {
                throw new NotFoundException("image_not_found", $"image with Id '{query.Id}' not found");
            }

            return new ImageDto
            {
                Content = image.content,
                MimeType = image.mimeType
            };
        }
    }
}
