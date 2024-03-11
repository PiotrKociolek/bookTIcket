using Template.Modules.Core.Application.Dto.Accounts;
using Template.Modules.Core.Application.Dto.Users;
using Template.Modules.Core.Application.Messages.Commands.Users;
using Template.Modules.Core.Application.Messages.Queries.Users;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Application.Types;
using Template.Modules.Shared.Core.Enums;
using Template.Modules.Shared.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Template.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Return data of current user.
        /// </summary>
        /// <param name="query">No params for this operation are required</param>
        [Authorize]
        [HttpGet("self")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSelf([FromQuery] GetUserSelf query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Change user password.
        /// </summary>
        /// <param name="command">Params for changing password operation</param>
        [HttpPut("self/change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword command)
        {
             await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Return data of selected user.
        /// </summary>
        /// <param name="id">Param for getting user by id operation</param>
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var query = new GetUser(id);

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Edit data of selected user.
        /// </summary>
        /// <param name="command">Params for editing user operation</param>
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUser([FromBody] EditUser command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Delete selected user.
        /// </summary>
        /// <param name="id">Param for deleting user operation</param>
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
        {
            var command = new DeleteUser(id);

            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Return list of users.
        /// </summary>
        /// <param name="query">Params for listing users</param>
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpGet("list")]
        [ProducesResponseType(typeof(PagedResults<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BrowseUsers([FromQuery] BrowseUsers query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="command">Params for creating new user</param>
        [Authorize(Roles = nameof(RoleEnum.Admin))]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUser command)
        {
            command.Bind(x => x.Id, Guid.NewGuid());
            await _mediator.Send(command);

            return Created($"account/{command.Id}", new { Id = command.Id });
        }

        /// <summary>
        /// Validate if e-mail is already in use.
        /// </summary>
        /// <param name="query">Params for validating user e-mail</param>
        [HttpGet("validate-email")]
        [ProducesResponseType(typeof(ValidateEmailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateEmail([FromQuery] ValidateEmail query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Send reset password token on user e-mail.
        /// </summary>
        /// <param name="command">Params for resetting user password</param>
        [HttpPut("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Set new password for a user.
        /// </summary>
        /// <param name="command">Params for setting new password</param>
        [HttpPut("set-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetNewPassword([FromBody] SetNewPassword command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Validate if reset password token is correct.
        /// </summary>
        /// <param name="query">Params for validating reset password token</param>
        [HttpGet("reset-password")]
        [ProducesResponseType(typeof(ValidateResetPasswordTokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateResetPasswordToken([FromQuery] ValidateResetPasswordToken query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
