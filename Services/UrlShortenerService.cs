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
    
    public async Task<UrlShortResponse> AddUrlShortyAsync(UrlAddRequest? urlRequest)
    {
        ArgumentNullException.ThrowIfNull(urlRequest);
        ArgumentNullException.ThrowIfNull(urlRequest.UrlLong);

        UrlShortener initial = new UrlShortener()
        {
            LongUrl = urlRequest.UrlLong,
            Clicks = 0,
            CreatedAt = DateTime.Now
        };

        var id = await _urlShortenerRepository.AddInitialAsync(initial);

        if (!string.IsNullOrEmpty(id.ShortUrl))
        {
            return new UrlShortResponse(id.ShortUrl);
        }

        string hashedCode = _hashingService.HashUrl(urlRequest.UrlLong, id.UrlId);

        var final = await _urlShortenerRepository.UpdateShortUrl(hashedCode, urlRequest.UrlLong);

        return new UrlShortResponse(final!.ShortUrl);
    }

    public async Task<UrlLongResponse?> GetOriginalUrlAsync(UrlGetRequest? urlRequest)
    {
        ArgumentNullException.ThrowIfNull(urlRequest);
        
        ArgumentNullException.ThrowIfNull(urlRequest.UrlShort);

        var originalUrl = await _urlShortenerRepository
            .GetOriginalUrlAsync(urlRequest.UrlShort);

        return originalUrl is null ? null : new UrlLongResponse(originalUrl);
    }

    public async Task<UrlAnalyticsResponse?> GetAnalyticsAsync(UrlGetRequest? urlRequest)
    {
        ArgumentNullException.ThrowIfNull(urlRequest);
        var urlShort = urlRequest.UrlShort;
        ArgumentNullException.ThrowIfNull(urlShort);

        var urlResponse = await _urlShortenerRepository.GetAnalyticsAsync(urlShort);
        
        return urlResponse?.ToUrlShortyResponseFull();
    }

    public async Task<List<UrlAnalyticsResponse>> GetAllAnalyticsAsync()
    {
        var allUrls = (await _urlShortenerRepository.GetAllUrlShorties())
            .Select(u => u.ToUrlShortyResponseFull()).ToList();
        return allUrls;
    }
}