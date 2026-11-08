using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly AppDbContext _db;
    public ArticlesController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetPublished() =>
        Ok(await _db.Articles
            .Where(a => a.Status == ArticleStatus.Published)
            .OrderByDescending(a => a.PublishedAt)
            .ToListAsync());

    [HttpGet("{slug}")]
    public async Task<ActionResult<Article>> GetBySlug(string slug)
    {
        var article = await _db.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
        return article is null ? NotFound() : Ok(article);
    }

    [HttpGet("dashboard")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Article>>> GetAllForDashboard() =>
        Ok(await _db.Articles.OrderByDescending(a => a.Id).ToListAsync());

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Article>> Create(Article article)
    {
        if (article.Status == ArticleStatus.Published && !IsBilingualComplete(article))
            return BadRequest(new { message = "Both Arabic and English content are required before publishing." });

        if (article.Status == ArticleStatus.Published)
            article.PublishedAt = DateTime.UtcNow;

        _db.Articles.Add(article);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBySlug), new { slug = article.Slug }, article);
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, Article updated)
    {
        var article = await _db.Articles.FindAsync(id);
        if (article is null) return NotFound();

        if (updated.Status == ArticleStatus.Published && !IsBilingualComplete(updated))
            return BadRequest(new { message = "Both Arabic and English content are required before publishing." });

        article.TitleAr = updated.TitleAr;
        article.TitleEn = updated.TitleEn;
        article.ContentAr = updated.ContentAr;
        article.ContentEn = updated.ContentEn;
        article.ExcerptAr = updated.ExcerptAr;
        article.ExcerptEn = updated.ExcerptEn;
        article.Slug = updated.Slug;
        article.CoverImageUrl = updated.CoverImageUrl;
        article.Tags = updated.Tags;

        // Only stamp PublishedAt the moment it transitions into Published
        if (updated.Status == ArticleStatus.Published && article.Status != ArticleStatus.Published)
            article.PublishedAt = DateTime.UtcNow;

        article.Status = updated.Status;

        await _db.SaveChangesAsync();
        return Ok(article);
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var article = await _db.Articles.FindAsync(id);
        if (article is null) return NotFound();

        _db.Articles.Remove(article);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private static bool IsBilingualComplete(Article a) =>
        !string.IsNullOrWhiteSpace(a.TitleAr) && !string.IsNullOrWhiteSpace(a.TitleEn) &&
        !string.IsNullOrWhiteSpace(a.ContentAr) && !string.IsNullOrWhiteSpace(a.ContentEn);
}
