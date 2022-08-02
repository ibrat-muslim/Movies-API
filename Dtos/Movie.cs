using System.ComponentModel.DataAnnotations;

namespace moviesApi.Dtos;

public class Movie
{
    [Required]
    [MaxLength(255)]
    public string? Title { get; set; }
    [Required]
    [Range(typeof(DateTime), "1/1/1962", "1/1/2023")]
    public DateTime ReleaseDate { get; set; }
    [MaxLength(1024)]
    public string? Description { get; set; }
    [Required]
    [Range(0.0, 10.0)]
    public double Imdb { get; set; }
    [Required]
    public EGenre Genres { get; set; }    
}
