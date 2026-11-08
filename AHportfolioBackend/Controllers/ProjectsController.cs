using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.DTOs;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ProjectsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetAll() =>
        Ok(await _db.Projects.OrderBy(p => p.Order).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Project>> GetById(int id)
    {
        var project = await _db.Projects.FindAsync(id);
        return project is null ? NotFound() : Ok(project);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Project>> Create(ProjectCreateDto dto)
    {
        var project = new Project
        {
            TitleAr = dto.TitleAr,
            TitleEn = dto.TitleEn,
            DescAr = dto.DescAr,
            DescEn = dto.DescEn,
            ThumbnailUrl = dto.ThumbnailUrl,
            VideoUrl = dto.VideoUrl,
            GithubRepoUrl = dto.GithubRepoUrl,
            LiveDemoUrl = dto.LiveDemoUrl,
            Tags = string.Join(",", dto.Tags),
            Order = dto.Order
        };

        _db.Projects.Add(project);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, UpdateProjectDto dto)
    {
        var project = await _db.Projects.FindAsync(id);
        if (project is null) return NotFound();

        project.TitleAr = dto.TitleAr;
        project.TitleEn = dto.TitleEn;
        project.DescAr = dto.DescAr;
        project.DescEn = dto.DescEn;
        project.ThumbnailUrl = dto.ThumbnailUrl;
        project.VideoUrl = dto.VideoUrl;
        project.GithubRepoUrl = dto.GithubRepoUrl;
        project.LiveDemoUrl = dto.LiveDemoUrl;
        project.Tags = string.Join(",", dto.Tags);
        project.Order = dto.Order;

        await _db.SaveChangesAsync();
        return Ok(project);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _db.Projects.FindAsync(id);
        if (project is null) return NotFound();

        _db.Projects.Remove(project);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
