using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Services.Interfaces;
using System;
using System.Threading.Tasks;
using BookTicket.Model.Dtos.request;

namespace BookTicket.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddMovieAsync(MovieDto dto)
        {
            var newMovie = new Movie
            {
                Title = dto.Title
            };

            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync(); 

            return newMovie;
        }


        public void DeleteMovie(MovieDto dto)
        {
            var movieToDelete = new Movie
            {
                Id = dto.Id
            };
              _context.Movies.Remove(movieToDelete);
        }
    }
}