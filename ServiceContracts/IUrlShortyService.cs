using ServiceContracts.DTOs;

namespace ServiceContracts;

public interface IUrlShortyService
{
    Task<UrlShortyResponse> AddUrlShortyAsync(UrlShortyAddRequest? urlRequest);

    Task<UrlShortyResponse> GetOriginalUrlAsync(UrlShortyGetRequest? urlRequest);

    Task<UrlShortyResponseFull> GetOriginalUrlFullAsync(UrlShortyAddRequest? urlRequest);
}