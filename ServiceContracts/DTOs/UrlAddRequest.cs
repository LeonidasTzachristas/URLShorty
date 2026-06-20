using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTOs;

public record UrlAddRequest
{
    [Required]
    [Url]
    public string UrlLong { get; set; } = string.Empty;
}
