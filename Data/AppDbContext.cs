using Microsoft.EntityFrameworkCore;
using moviesApi.Entities;

namespace moviesApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
        
    public DbSet<Movie>? Movies { get; set; }    
}