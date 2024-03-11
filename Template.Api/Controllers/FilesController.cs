using Template.Modules.Core.Application.Messages.Queries;
using Template.Modules.Shared.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("image/{id}")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImage([FromRoute] string id)
        {
            var image = await _mediator.Send(new GetImage { Id = id });
            return File(image.Content, image.MimeType);
        }

        [HttpGet("image/assets/{id}")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImageFromAssets([FromRoute] string id)
        {
            var image = await _mediator.Send(new GetImage { Id = $"Assets/{id}" });
            return File(image.Content, image.MimeType);
        }
    }
}
