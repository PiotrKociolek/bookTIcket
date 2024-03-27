using System.Net;
using BookTicket.Model;
using BookTicket.Model.Dtos.Screening;
using BookTicket.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookTicket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScreeningController : ControllerBase
{
    private readonly IScreeningService _screeningService;

    public ScreeningController(IScreeningService screeningService)
    {
        _screeningService = screeningService;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    // This wont even work
    public Task AddScreeningAsync(AddScreeningDto dto)
    {
        return _screeningService.AddScreeningAsync(dto);
    }


    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public Task EditScreeningAsync(EditScreeningDto dto)
    {
        return _screeningService.EditScreeningAsync(dto);
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public Task DeleteScreeningAsync(DeleteScreeningDto dto)
    {
        return _screeningService.DeleteScreeningAsync(dto);
    }

    [HttpGet("GetScreeningBy/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public Task<Screening> GetScreeningByIdAsync(int id)
    {
        return _screeningService.GetScreeningByIdAsync(id);
    }

    [HttpGet("GetScreeningDetails/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public Task<Screening> GetScreeningDetailsAsync(int id)
    {
        return _screeningService.GetScreeningDetailsAsync(id);
    }

    [HttpGet("getAllScreenings")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public Task<List<Screening>> GetAllScreeningsAsync()
    {
        return _screeningService.GetAllScreeningsAsync();
    }
}