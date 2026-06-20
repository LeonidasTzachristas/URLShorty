using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTOs;

namespace URLShortener.Controllers;

[ApiController]
[Route("api/urls")]
public class UrlController : ControllerBase
{
    private readonly IUrlShortenerService _urlShortenerService;

    public UrlController(IUrlShortenerService urlShortenerService)
    {
        _urlShortenerService = urlShortenerService;
    }


    [HttpGet("{urlShort}")]
    public async Task<IActionResult> GetOriginalUrl
        ([FromRoute]UrlGetRequest urlRequest)
    {
        var result = await _urlShortenerService
            .GetOriginalUrlAsync(urlRequest);
        
        if (result?.Url is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpGet("{urlShort}/analytics")]
    public async Task<IActionResult> GetAnalytics
        ([FromRoute]UrlGetRequest urlRequest)
    {
        var result = await _urlShortenerService
            .GetAnalyticsAsync(urlRequest);

        if (result is null)
            return NotFound();
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUrls()
    {
        var urls = await _urlShortenerService.GetAllAnalyticsAsync();
        return Ok(urls);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUrl
        ([FromBody]UrlAddRequest urlAddRequest)
    {
        var result = await _urlShortenerService.AddUrlAsync(urlAddRequest);
        
        return CreatedAtAction(
            nameof(GetOriginalUrl),
            new { urlShort = result.Url},
            result);
    }
}