using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTOs;

namespace URLShorty.Controllers;

[ApiController]
[Route("api/urls")]
public class UrlShortyController : ControllerBase
{
    private readonly IUrlShortyService _urlShortyService;

    public UrlShortyController(IUrlShortyService urlShortyService)
    {
        _urlShortyService = urlShortyService;
    }


    [HttpGet("{urlShort}")]
    public async Task<IActionResult> GetOriginalUrl(string? urlShort)
    {
        if (string.IsNullOrEmpty(urlShort))
            return BadRequest();
            
        var temp = new UrlShortyGetRequest(urlShort);
        var result = await _urlShortyService
            .GetOriginalUrlAsync(temp);
        
        if (string.IsNullOrEmpty(result.UrlLong))
        {
            return NotFound();
        }
        return RedirectPermanent(result.UrlLong);
    }

    [HttpGet("{urlShort}/analytics")]
    public async Task<IActionResult> GetAnalytics(string? urlShort)
    {
        if (string.IsNullOrEmpty(urlShort))
            return BadRequest();

        var temp = new UrlShortyGetRequest(urlShort);
        var result = await _urlShortyService
            .GetAnalyticsAsync(temp);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUrls()
    {
        var temp = await _urlShortyService.GetAllAnalyticsAsync();
        return Ok(temp);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUrl
        ([FromBody]UrlShortyAddRequest urlLong)
    {
        if (string.IsNullOrEmpty(urlLong.UrlLong))
            return BadRequest();
        
        var result = await _urlShortyService.AddUrlShortyAsync(urlLong);
        
        return Ok(result);
    }
}