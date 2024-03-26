using BookTicket.Model;
using BookTicket.Model.Dtos.Movie;

namespace BookTicket.Services.Interfaces;

public interface IMovieService
{
    Task<Movie> AddMovieAsync(MovieDto dto);
    Task<Movie> GetMovieAsync(int id);
    Task<List<Movie>> GetAllMoviesAsync();
    void DeleteMovie(MovieDto dto);
}