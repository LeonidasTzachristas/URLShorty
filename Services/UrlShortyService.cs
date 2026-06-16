using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.DTOs.Extensions;

namespace Services;

public class UrlShortyService : IUrlShortyService
{
    private readonly IUrlShortyRepository _urlShortyRepository;
    private readonly IHashingService _hashingService;

    public UrlShortyService(IUrlShortyRepository urlShortyRepository, 
        IHashingService hashingService)
    {
        _urlShortyRepository = urlShortyRepository;
        _hashingService = hashingService;
    }

    
    public async Task<UrlShortyResponse> AddUrlShortyAsync(UrlShortyAddRequest? urlRequest)
    {
        ArgumentNullException.ThrowIfNull(urlRequest);
        ArgumentNullException.ThrowIfNull(urlRequest.UrlLong);

        UrlShorty initial = new UrlShorty()
        {
            LongUrl = urlRequest.UrlLong,
            Clicks = 0,
            CreatedAt = DateTime.Now
        };

        var id = await _urlShortyRepository.AddInitialAsync(initial);

        string hashedCode = _hashingService.HashUrl(urlRequest.UrlLong, id);

        var final = await _urlShortyRepository.UpdateShortUrl(hashedCode, urlRequest.UrlLong);

        return new UrlShortyResponse(final!.ShortUrl);
    }

    public async Task<UrlShortyResponse> GetOriginalUrlAsync(UrlShortyGetRequest? urlRequest)
    {
        ArgumentNullException.ThrowIfNull(urlRequest);
        
        ArgumentNullException.ThrowIfNull(urlRequest.UrlShort);

        var originalUrl = await _urlShortyRepository
            .GetOriginalUrlAsync(urlRequest.UrlShort);

        if (string.IsNullOrEmpty(originalUrl))
            throw new KeyNotFoundException();

        return new UrlShortyResponse(originalUrl);
    }

    public async Task<UrlShortyResponseFull?> GetAnalyticsAsync(UrlShortyGetRequest? urlRequest)
    {
        ArgumentNullException.ThrowIfNull(urlRequest);
        var urlShort = urlRequest.UrlShort;
        ArgumentNullException.ThrowIfNull(urlShort);

        var urlResponse = await _urlShortyRepository.GetAnalyticsAsync(urlShort);
        
        return urlResponse?.ToUrlShortyResponseFull();
    }

    public async Task<List<UrlShortyResponseFull>> GetAllAnalyticsAsync()
    {
        var allUrls = (await _urlShortyRepository.GetAllUrlShorties())
            .Select(u => u.ToUrlShortyResponseFull()).ToList();
        return allUrls;
    }
}