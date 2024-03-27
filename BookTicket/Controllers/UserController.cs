using System.Net;
using BookTicket.Model.Dto_s.request;
using BookTicket.Model.Dtos.User;
using BookTicket.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTicket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser(RegisterRequestDto dto)
    {
        var newUser = await _userService.RegisterUserAsync(dto);
        return Ok(newUser);
    }

    [HttpPost("login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [AllowAnonymous]
    public async Task<IActionResult> LoginUser(LoginRequestDto dto)
    {
        var token = await _userService.LoginUserAsync(dto);
        return Ok(new { token });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [Authorize]
    public async Task<IActionResult> DeleteUserByIdAsync(int id)
    {
        await _userService.DeleteUserByIdAsync(id);
        return Ok();
    }
}