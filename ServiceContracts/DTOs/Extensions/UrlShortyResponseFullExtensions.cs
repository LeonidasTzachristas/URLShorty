using Entities;

namespace ServiceContracts.DTOs.Extensions;

public static class UrlShortyResponseFullExtensions
{
    public static UrlShortyResponseFull ToUrlShortyResponseFull(this UrlShorty urlShorty)
    {
        return new UrlShortyResponseFull()
        {
            LongUrl = urlShorty.LongUrl,
            ShortUrl = urlShorty.ShortUrl,
            CreatedAt = urlShorty.CreatedAt,
            Clicks = urlShorty.Clicks
        };
    }
}