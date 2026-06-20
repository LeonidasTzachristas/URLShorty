using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.DTOs.Extensions;

namespace Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly IUrlShortenerRepository _urlShortenerRepository;
    private readonly IHashingService _hashingService;

    public UrlShortenerService(IUrlShortenerRepository urlShortenerRepository, 
        IHashingService hashingService)
    {
        _urlShortenerRepository = urlShortenerRepository;
        _hashingService = hashingService;
    }
    
    public async Task<UrlResponse> AddUrlAsync(UrlAddRequest urlRequest)
    {
        if (string.IsNullOrEmpty(urlRequest.UrlLong))
            return null;
            
        var initial = new UrlShortener()
        {
            LongUrl = urlRequest.UrlLong,
            Clicks = 0,
            CreatedAt = DateTime.Now
        };

        var tempUrl = await _urlShortenerRepository.AddInitialAsync(initial);

        if (!string.IsNullOrEmpty(tempUrl.ShortUrl))
        {
            return new UrlResponse(tempUrl.ShortUrl);
        }

        string hashedCode = _hashingService.HashUrl(urlRequest.UrlLong, tempUrl.UrlId);

        var final = await _urlShortenerRepository.UpdateShortUrlAsync(hashedCode, urlRequest.UrlLong);

        return new UrlResponse(final!.ShortUrl);
    }

    public async Task<UrlResponse?> GetOriginalUrlAsync(UrlGetRequest urlRequest)
    {
        var originalUrl = await _urlShortenerRepository
            .GetByShortUrlAsync(urlRequest.UrlShort);

        return originalUrl is null ? null : new UrlResponse(originalUrl.LongUrl);
    }

    public async Task<UrlAnalyticsResponse?> GetAnalyticsAsync(UrlGetRequest urlRequest)
    {
        ArgumentNullException.ThrowIfNull(urlRequest);
        var urlShort = urlRequest.UrlShort;
        ArgumentNullException.ThrowIfNull(urlShort);

        var urlResponse = await _urlShortenerRepository.GetByShortUrlAsync(urlShort);
        
        return urlResponse?.ToUrlShortyResponseFull();
    }

    public async Task<List<UrlAnalyticsResponse>> GetAllAnalyticsAsync()
    {
        var allUrls = (await _urlShortenerRepository.GetAllUrlsAsync())
            .Select(u => u.ToUrlShortyResponseFull()).ToList();
        return allUrls;
    }
}