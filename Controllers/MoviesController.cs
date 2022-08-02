using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moviesApi.Data;
using moviesApi.Entities;

namespace moviesApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext? _context;
    private readonly ILogger<MoviesController>? _logger;

    public MoviesController(
        ILogger<MoviesController> logger,
        AppDbContext context
    )
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _context.Movies.ToListAsync());

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute]Guid id)
    {
        var entity = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        entity.Viewed++;

        await _context.SaveChangesAsync();

        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm]Dtos.Movie movie)
    {
        var entity = new Movie()
        {
            Id = Guid.NewGuid(),
            Title = movie.Title,
            ReleaseDate = movie.ReleaseDate,
            Description = movie.Description,
            Imdb = movie.Imdb,
            Genre = movie.Genres switch
            {
                Dtos.EGenre.Action => EGenre.Action,
                Dtos.EGenre.Comedy => EGenre.Comedy,
                Dtos.EGenre.Drama => EGenre.Drama,
                Dtos.EGenre.Fantasy => EGenre.Fantasy,
                Dtos.EGenre.Horror => EGenre.Horror
            }
        };

        await _context.Movies.AddAsync(entity);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Post), movie);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var entity = await _context.Movies.FindAsync(id);
        if(entity is null) return NotFound();

        _context.Movies.Remove(entity);

        await _context.SaveChangesAsync();

        return Accepted();
    }
}
