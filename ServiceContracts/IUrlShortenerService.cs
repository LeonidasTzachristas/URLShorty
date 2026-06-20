using ServiceContracts.DTOs;

namespace ServiceContracts;

public interface IUrlShortenerService
{
    Task<UrlResponse> AddUrlAsync(UrlAddRequest urlRequest);

    Task<UrlResponse?> GetOriginalUrlAsync(UrlGetRequest urlRequest);

    Task<UrlAnalyticsResponse?> GetAnalyticsAsync(UrlGetRequest urlRequest);

    Task<List<UrlAnalyticsResponse>> GetAllAnalyticsAsync();
}