using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories;

public class UrlShortenerRepository : IUrlShortenerRepository
{
    private readonly UrlShortenerDbContext _db;

    public UrlShortenerRepository(UrlShortenerDbContext db)
    {
        _db = db;
    }

    public async Task<string?> GetOriginalUrlAsync(string urlShort)
    {
        var url = await _db.Urls.AsNoTracking().FirstOrDefaultAsync(u => 
            u.ShortUrl == urlShort);

        if (url is null)
            return null;

        await _db.Database.ExecuteSqlInterpolatedAsync(
            $"""
             UPDATE Urls
             SET Clicks = Clicks + 1
             WHERE ShortUrl = {urlShort}
             """);
        
        return url.LongUrl;
    }

    public async Task<UrlShortener?> GetAnalyticsAsync(string urlShort)
    {
        var url = await _db.Urls.AsNoTracking().FirstOrDefaultAsync(u => 
            u.ShortUrl == urlShort);
        
        return url;
    }

    public async Task<List<UrlShortener>> GetAllUrlShorties()
    {
        return await _db.Urls.AsNoTracking().ToListAsync();
    }


    public async Task<UrlShortener> AddInitialAsync(UrlShortener urlShortener)
    {
        var temp = await _db.Urls.FirstOrDefaultAsync(u => 
            u.LongUrl == urlShortener.LongUrl);
        if (temp is not null)
            return temp;
        
        var url = await _db.Urls.AddAsync(urlShortener);
        await _db.SaveChangesAsync();

        return url.Entity;
    }

    public async Task<UrlShortener?> UpdateShortUrl(string urlShort, string urlLong)
    {
        var urlToUpdate = await _db.Urls.FirstOrDefaultAsync(u =>
            u.LongUrl == urlLong);

        if (urlToUpdate is null)
            return null;

        urlToUpdate.ShortUrl = urlShort;
        await _db.SaveChangesAsync();

        return urlToUpdate;
    }
}