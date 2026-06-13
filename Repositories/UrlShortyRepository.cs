using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories;

public class UrlShortyRepository : IUrlShortyRepository
{
    private readonly UrlShortyDbContext _db;

    public UrlShortyRepository(UrlShortyDbContext db)
    {
        _db = db;
    }

    public async Task<string?> GetOriginalUrlAsync(string urlShort)
    {
        var url = await _db.Urls.FirstOrDefaultAsync(u => 
            u.ShortUrl.Equals(urlShort));
        
        if (url is not null)
        {
            url.Clicks++;
            await _db.SaveChangesAsync();
        }
        
        return url?.LongUrl;
    }

    public async Task<UrlShorty?> GetByShortCodeFullAsync(string urlShort)
    {
        var url = await _db.Urls.FirstOrDefaultAsync(u => 
            u.ShortUrl.Equals(urlShort));
        
        return url;
    }

    public async Task<List<UrlShorty>> GetAllUrlShorties()
    {
        return await _db.Urls.ToListAsync();
    }


    public async Task<long> AddInitialAsync(UrlShorty urlShorty)
    {
        var url = await _db.Urls.AddAsync(urlShorty);
        await _db.SaveChangesAsync();

        return url.Entity.UrlId;
    }

    public async Task<UrlShorty?> UpdateShortUrl(string urlShort, string urlLong)
    {
        var urlToUpdate = await _db.Urls.FirstOrDefaultAsync(u =>
            u.LongUrl.Equals(urlLong));

        if (urlToUpdate is null)
            return null;

        urlToUpdate.ShortUrl = urlShort;
        await _db.SaveChangesAsync();

        return urlToUpdate;
    }

    public async Task<bool> ExistsAsync(string urlShort)
    {
        return await _db.Urls.AnyAsync(u => u.ShortUrl.Equals(urlShort));
    }
}