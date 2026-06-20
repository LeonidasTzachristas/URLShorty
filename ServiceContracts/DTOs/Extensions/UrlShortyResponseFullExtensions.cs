using Entities;

namespace ServiceContracts.DTOs.Extensions;

public static class UrlShortyResponseFullExtensions
{
    public static UrlAnalyticsResponse ToUrlShortyResponseFull(this UrlShortener urlShortener)
    {
        return new UrlAnalyticsResponse()
        {
            LongUrl = urlShortener.LongUrl,
            ShortUrl = urlShortener.ShortUrl,
            CreatedAt = urlShortener.CreatedAt,
            Clicks = urlShortener.Clicks
        };
    }
}