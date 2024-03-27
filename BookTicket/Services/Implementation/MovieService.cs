using BookTicket.Data;
using BookTicket.Model;
using BookTicket.Services.Interfaces;
using System;
using System.Threading.Tasks;
using BookTicket.Model.Dtos.Movie;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Movie> GetMovieAsync(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            return movie;
        }


        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        public void DeleteMovie(MovieDto dto)
        {
            var movieToDelete = new Movie
            {
                Id = dto.Id
            };
            _context.Movies.Where(x => x.Id == dto.Id);
            //_context.Movies.Remove(movieToDelete);
        }
    }
}