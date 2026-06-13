using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTOs;

public record UrlShortyAddRequest([Url]string? UrlLong);
