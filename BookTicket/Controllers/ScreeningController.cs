using System.Net;
using BookTicket.Model.Dtos.Screening;
using BookTicket.Services.Interfaces;
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
    Task AddScreeningAsync(AddScreeningDto dto)
    {
        return _screeningService.AddScreeningAsync(dto);
    }

   
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    Task EditScreeningAsync(EditScreeningDto dto)
    {
        return _screeningService.EditScreeningAsync(dto);
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    Task DeleteScreeningAsync(DeleteScreeningDto dto)
    {
        return _screeningService.DeleteScreeningAsync(dto);
    }
}