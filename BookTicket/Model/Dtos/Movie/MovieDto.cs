using System.ComponentModel.DataAnnotations;

namespace BookTicket.Model.Dtos.Movie;

public class MovieDto
{
    public string Title { get; set; }
    public int Id { get; set; }
}