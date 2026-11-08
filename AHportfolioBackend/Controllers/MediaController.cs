using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Services;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MediaController : ControllerBase
{
    private readonly IFileUploadService _fileUploadService;
    public MediaController(IFileUploadService fileUploadService) => _fileUploadService = fileUploadService;

    [HttpPost("upload")]
    [RequestSizeLimit(200_000_000)] // 200MB, generous enough for demo videos
    public async Task<IActionResult> Upload(IFormFile file, [FromQuery] string type = "images")
    {
        if (file.Length == 0) return BadRequest(new { message = "No file received." });

        var url = await _fileUploadService.SaveFileAsync(file, type);
        return Ok(new { url });
    }
}
