using System.Net;
using BookTicket.Model;
using BookTicket.Model.Dtos.Ticket;
using BookTicket.Services.Implementation;
using BookTicket.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookTicket.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public Task<Ticket> AddTicketAsync(TicketDto dto)
    {
        return _ticketService.AddTicketAsync(dto);
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [Authorize("Admin")]
    public void DeleteTicketById(int id)
    {
        _ticketService.DeleteTicketById(id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)][Authorize("Admin")]

    public async Task<ActionResult<Ticket>> GetTicket(int id)
    {
        var ticket = await _ticketService.GetTicketDataAsync(id);
        return ticket;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [Authorize("Admin")]
    public async Task<ActionResult<List<Ticket>>> GetTicketsByUser(int userId)
    {
        var tickets = await _ticketService.GetTicketsByUserAsync(userId);

        if (tickets == null || tickets.Count == 0)
        {
            return NotFound();
        }

        return tickets;
    }
}