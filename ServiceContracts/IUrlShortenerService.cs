using ServiceContracts.DTOs;

namespace ServiceContracts;

public interface IUrlShortenerService
{
    Task<UrlShortResponse> AddUrlShortyAsync(UrlAddRequest? urlRequest);

    Task<UrlLongResponse?> GetOriginalUrlAsync(UrlGetRequest? urlRequest);

    Task<UrlAnalyticsResponse?> GetAnalyticsAsync(UrlGetRequest? urlRequest);

    Task<List<UrlAnalyticsResponse>> GetAllAnalyticsAsync();
}