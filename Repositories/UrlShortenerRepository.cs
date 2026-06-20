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
    
    public async Task<UrlShortener?> GetByShortUrlAsync(string urlShort)
    {
        var url = await _db.Urls.FirstOrDefaultAsync(u => 
            u.ShortUrl == urlShort);

        if (url is not null)
        {
            url.Clicks++;
            await _db.SaveChangesAsync();
        }
        
        return url;
    }

    public async Task<List<UrlShortener>> GetAllUrlsAsync()
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

    public async Task<UrlShortener?> UpdateShortUrlAsync(string urlShort, string urlLong)
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