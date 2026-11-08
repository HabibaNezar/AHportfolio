using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Data;
using PortfolioAPI.DTOs;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutController : ControllerBase
{
    private readonly AppDbContext _db;
    public AboutController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<AboutDto>> Get()
    {
        var about = await _db.Abouts
            .Include(a => a.SkillCategories.OrderBy(c => c.Id)).ThenInclude(c => c.Items.OrderBy(i => i.Id))
            .Include(a => a.Experiences.OrderBy(e => e.Id))
            .FirstOrDefaultAsync();

        if (about is null)
        {
            about = new About();
            _db.Abouts.Add(about);
            await _db.SaveChangesAsync();
        }

        return Ok(MapToDto(about));
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(AboutDto dto)
    {
        var about = await _db.Abouts
            .Include(a => a.SkillCategories).ThenInclude(c => c.Items)
            .Include(a => a.Experiences)
            .FirstOrDefaultAsync();

        if (about is null)
        {
            about = new About();
            _db.Abouts.Add(about);
        }

        about.Text = dto.About;
        SyncSkillCategories(about, dto.SkillCategories);
        SyncExperiences(about, dto.Experiences);

        await _db.SaveChangesAsync();
        return Ok(MapToDto(about));
    }

    [HttpPost("skills")]
    [Authorize]
    public async Task<ActionResult<SkillCategoryDto>> AddSkillCategory(SkillCategoryCreateDto dto)
    {
        var about = await GetOrCreateAboutAsync();

        var category = new SkillCategory
        {
            Name = dto.Name,
            Description = dto.Description,
            AboutId = about.Id
        };

        _db.SkillCategories.Add(category);
        await _db.SaveChangesAsync();
        return Ok(MapCategoryToDto(category));
    }

    [HttpPost("skills/{categoryId:int}/items")]
    [Authorize]
    public async Task<ActionResult<SkillItemDto>> AddSkillItem(int categoryId, SkillItemCreateDto dto)
    {
        var about = await GetOrCreateAboutAsync();
        var category = await _db.SkillCategories
            .FirstOrDefaultAsync(c => c.Id == categoryId && c.AboutId == about.Id);
        if (category is null) return NotFound();

        var item = new SkillItem
        {
            Name = dto.Name,
            Icon = dto.Icon,
            SkillCategoryId = category.Id
        };

        _db.SkillItems.Add(item);
        await _db.SaveChangesAsync();
        return Ok(new SkillItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Icon = item.Icon,
            SkillCategoryId = item.SkillCategoryId,
            CategoryName = category.Name
        });
    }

    [HttpDelete("skills/{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteSkillCategory(int id)
    {
        var about = await GetOrCreateAboutAsync();
        var category = await _db.SkillCategories
            .FirstOrDefaultAsync(c => c.Id == id && c.AboutId == about.Id);
        if (category is null) return NotFound();

        _db.SkillCategories.Remove(category);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("skillitems/{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteSkillItem(int id)
    {
        var item = await _db.SkillItems.FindAsync(id);
        if (item is null) return NotFound();

        _db.SkillItems.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("experiences")]
    [Authorize]
    public async Task<ActionResult<ExperienceDto>> AddExperience(ExperienceCreateDto dto)
    {
        var about = await GetOrCreateAboutAsync();

        var experience = new Experience
        {
            From = dto.From,
            To = dto.To,
            Title = dto.Title,
            Description = dto.Description,
            AboutId = about.Id
        };

        _db.Experiences.Add(experience);
        await _db.SaveChangesAsync();
        return Ok(MapExperienceToDto(experience));
    }

    [HttpDelete("experiences/{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteExperience(int id)
    {
        var about = await GetOrCreateAboutAsync();
        var experience = await _db.Experiences
            .FirstOrDefaultAsync(e => e.Id == id && e.AboutId == about.Id);
        if (experience is null) return NotFound();

        _db.Experiences.Remove(experience);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    private async Task<About> GetOrCreateAboutAsync()
    {
        var about = await _db.Abouts.FirstOrDefaultAsync();
        if (about is null)
        {
            about = new About();
            _db.Abouts.Add(about);
            await _db.SaveChangesAsync();
        }
        return about;
    }

    private static SkillCategoryDto MapCategoryToDto(SkillCategory category) => new()
    {
        Id = category.Id,
        Name = category.Name,
        Description = category.Description,
        AboutId = category.AboutId,
        Items = category.Items.Select(i => new SkillItemDto
        {
            Id = i.Id,
            Name = i.Name,
            Icon = i.Icon,
            SkillCategoryId = i.SkillCategoryId,
            CategoryName = category.Name
        }).ToList()
    };

    private static ExperienceDto MapExperienceToDto(Experience experience) => new()
    {
        Id = experience.Id,
        From = experience.From,
        To = experience.To,
        Title = experience.Title,
        Description = experience.Description,
        AboutId = experience.AboutId
    };

    private static AboutDto MapToDto(About about) => new()
    {
        Id = about.Id,
        About = about.Text,
        SkillCategories = about.SkillCategories.Select(c => new SkillCategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            AboutId = c.AboutId,
            Items = c.Items.Select(i => new SkillItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Icon = i.Icon,
                SkillCategoryId = i.SkillCategoryId,
                CategoryName = c.Name
            }).ToList()
        }).ToList(),
        Experiences = about.Experiences.Select(e => new ExperienceDto
        {
            Id = e.Id,
            From = e.From,
            To = e.To,
            Title = e.Title,
            Description = e.Description,
            AboutId = e.AboutId
        }).ToList()
    };

    private static void SyncSkillCategories(About about, List<SkillCategoryDto> categories)
    {
        var requestedIds = categories.Where(c => c.Id > 0).Select(c => c.Id).ToList();
        about.SkillCategories.RemoveAll(c => !requestedIds.Contains(c.Id));

        foreach (var dto in categories)
        {
            var category = dto.Id > 0 ? about.SkillCategories.FirstOrDefault(c => c.Id == dto.Id) : null;
            if (category is null)
            {
                category = new SkillCategory();
                about.SkillCategories.Add(category);
            }

            category.Name = dto.Name;
            category.Description = dto.Description;
            SyncSkillItems(category, dto.Items);
        }
    }

    private static void SyncSkillItems(SkillCategory category, List<SkillItemDto> items)
    {
        var requestedIds = items.Where(i => i.Id > 0).Select(i => i.Id).ToList();
        category.Items.RemoveAll(i => !requestedIds.Contains(i.Id));

        foreach (var dto in items)
        {
            var item = dto.Id > 0 ? category.Items.FirstOrDefault(i => i.Id == dto.Id) : null;
            if (item is null)
            {
                item = new SkillItem();
                category.Items.Add(item);
            }

            item.Name = dto.Name;
            item.Icon = dto.Icon;
        }
    }

    private static void SyncExperiences(About about, List<ExperienceDto> experiences)
    {
        var requestedIds = experiences.Where(e => e.Id > 0).Select(e => e.Id).ToList();
        about.Experiences.RemoveAll(e => !requestedIds.Contains(e.Id));

        foreach (var dto in experiences)
        {
            var experience = dto.Id > 0 ? about.Experiences.FirstOrDefault(e => e.Id == dto.Id) : null;
            if (experience is null)
            {
                experience = new Experience();
                about.Experiences.Add(experience);
            }

            experience.From = dto.From;
            experience.To = dto.To;
            experience.Title = dto.Title;
            experience.Description = dto.Description;
        }
    }
}
