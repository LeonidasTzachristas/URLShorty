namespace ServiceContracts.DTOs;

public record UrlShortyResponseFull
{
    public string? LongUrl { get; set; }

    public string? ShortUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public ulong Clicks { get; set; }
}