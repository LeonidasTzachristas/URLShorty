using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Entities;

[Index(nameof(LongUrl), IsUnique = true)]
[Index(nameof(ShortUrl), IsUnique = true)]
public class UrlShorty
{
    [Key]
    public long UrlId { get; set; }

    [StringLength(200)]
    public required string LongUrl { get; set; }

    [StringLength(7, MinimumLength = 7)] // Length exactly 6
    public string ShortUrl { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public long Clicks { get; set; }
}