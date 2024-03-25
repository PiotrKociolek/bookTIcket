using BookTicket.Model;
using BookTicket.Model.Dtos.request;

namespace BookTicket.Services.Interfaces;

public interface IMovieService
{
    Task<Movie> AddMovieAsync(MovieDto dto);
    public void DeleteMovie(MovieDto dto);
}