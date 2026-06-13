using Microsoft.EntityFrameworkCore;

namespace Entities;

public class UrlShortyDbContext : DbContext
{
    public DbSet<UrlShorty> Urls => Set<UrlShorty>();
    
    public UrlShortyDbContext(DbContextOptions<UrlShortyDbContext> options)
        : base(options)
    {
    }
}