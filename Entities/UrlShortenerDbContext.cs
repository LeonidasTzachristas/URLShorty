using Microsoft.EntityFrameworkCore;

namespace Entities;

public class UrlShortenerDbContext : DbContext
{
    public DbSet<UrlShortener> Urls => Set<UrlShortener>();
    
    public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}