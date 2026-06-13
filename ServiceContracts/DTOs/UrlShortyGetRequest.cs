using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTOs;

public record UrlShortyGetRequest(
    [StringLength(7, MinimumLength = 7)]
    string? UrlShort
    );