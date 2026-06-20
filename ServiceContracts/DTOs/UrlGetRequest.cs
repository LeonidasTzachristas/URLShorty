using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTOs;

public record UrlGetRequest
{
    [Required]
    [StringLength(7, MinimumLength = 7)]
    public String UrlShort { get; set; } = string.Empty;
}