using System.Net;
using BookTicket.Model.Dto_s.request;
using BookTicket.service;
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
    public async Task<IActionResult> RegisterUser(RegisterRequestDto dto)
    {
        try
        {
            var newUser = await _userService.RegisterUserAsync(dto);
            return Ok(newUser);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("already exists"))
                return BadRequest(ex.Message);
            else
                return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginRequestDto dto)
    {
        try
        {
            var token = await _userService.LoginUserAsync(dto);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}