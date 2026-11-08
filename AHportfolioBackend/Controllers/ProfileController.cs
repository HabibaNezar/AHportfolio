using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProfileController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<Profile>> Get()
    {
        var profile = await _db.Profiles.FirstOrDefaultAsync();
        return profile is null ? NotFound() : Ok(profile);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(Profile updated)
    {
        var profile = await _db.Profiles.FirstOrDefaultAsync();
        if (profile is null) return NotFound();

        profile.NameAr = updated.NameAr;
        profile.NameEn = updated.NameEn;
        profile.BioAr = updated.BioAr;
        profile.BioEn = updated.BioEn;
        profile.AvatarUrl = updated.AvatarUrl;
        profile.ResumeUrl = updated.ResumeUrl;
        profile.GithubUrl = updated.GithubUrl;
        profile.LinkedInUrl = updated.LinkedInUrl;

        await _db.SaveChangesAsync();
        return Ok(profile);
    }
}
