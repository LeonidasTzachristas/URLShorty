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
        var result = await _urlShortyService.GetOriginalUrlAsync(temp);
        if (string.IsNullOrEmpty(result.UrlLong))
        {
            return BadRequest();
        }
        return RedirectPermanent(result.UrlLong);
    }

    [HttpGet("all")]
    public IActionResult GetAllUrls()
    {
        return Ok();
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