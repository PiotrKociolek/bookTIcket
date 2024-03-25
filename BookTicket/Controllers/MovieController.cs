using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookTicket.Model;
using BookTicket.Services.Interfaces;
using System.Net;
using System.Threading.Tasks;
using BookTicket.Model.Dtos.request;
using BookTicket.Model.Flag;

namespace BookTicket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<Movie> AddMovieAsync(MovieDto dto)
        {
            return await _movieService.AddMovieAsync(dto);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public void DeleteMovie(MovieDto dto)
        {
            _movieService.DeleteMovie(dto);
        }
    }
}