using ServiceContracts.DTOs;

namespace ServiceContracts;

public interface IUrlShortyService
{
    Task<UrlShortyResponse> AddUrlShortyAsync(UrlShortyAddRequest? urlRequest);

    Task<UrlShortyResponse> GetOriginalUrlAsync(UrlShortyGetRequest? urlRequest);

    Task<UrlShortyResponseFull?> GetAnalyticsAsync(UrlShortyGetRequest? urlRequest);

    Task<List<UrlShortyResponseFull>> GetAllAnalyticsAsync();
}