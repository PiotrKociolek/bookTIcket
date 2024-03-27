using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookTicket.Model;
using BookTicket.Services.Interfaces;
using System.Net;
using System.Threading.Tasks;
using BookTicket.Model.Dtos.Movie;
using BookTicket.Model.Flag;

namespace BookTicket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Admin")]
    // Include ActionResult
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        // Post Should return Created instead of OK
        public async Task<ActionResult<Movie>> AddMovieAsync(MovieDto dto)
        {
            return await _movieService.AddMovieAsync(dto);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        // return NoContent()
        public void DeleteMovie(MovieDto dto)
        {
            _movieService.DeleteMovie(dto);
        }

        [HttpGet("GetMovie/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        // Return notfound when movie is null
        public async Task<ActionResult<Movie>> GetMovieAsync(int id)
        {
            var result = await _movieService.GetMovieAsync(id);

            if(result is null) {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetAllMovies")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await _movieService.GetAllMoviesAsync();
        }
    }
}