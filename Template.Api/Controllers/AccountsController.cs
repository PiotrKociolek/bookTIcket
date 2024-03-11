using Template.Modules.Core.Application.Dto.Accounts;
using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Core.Application.Messages.Commands.Users;
using Template.Modules.Shared.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Log in user.
        /// </summary>
        /// <param name="command">Params for logging in user</param>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoggedUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginUser([FromBody] LoginUser command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        /// <summary>
        /// Return new access token and set new refresh token.
        /// </summary>
        /// <param name="query">Params for refreshing user tokens</param>
        [HttpGet("refresh-token")]
        [ProducesResponseType(typeof(TokensDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromQuery] RefreshToken query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Logout user and delete refresh token from database.
        /// </summary>
        /// <param name="id">Param for logging out user</param>
        [Authorize]
        [HttpPost("logout/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogoutUser([FromRoute] Guid id)
        {
            var command = new LogoutUser(id);
            await _mediator.Send(command);

            return Ok();
        }
    }
}