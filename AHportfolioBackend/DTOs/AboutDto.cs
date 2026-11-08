using System.Text.Json.Serialization;

namespace PortfolioAPI.DTOs;

public class AboutDto
{
    public int Id { get; set; }
    public string About { get; set; } = string.Empty;
    public List<SkillCategoryDto> SkillCategories { get; set; } = new();
    public List<ExperienceDto> Experiences { get; set; } = new();
}

public class SkillCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("about")]
    public string Description { get; set; } = string.Empty;
    public int AboutId { get; set; }
    public List<SkillItemDto> Items { get; set; } = new();
}

public class SkillItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int SkillCategoryId { get; set; }
    [JsonPropertyName("skillCategory")]
    public string CategoryName { get; set; } = string.Empty;
}

public class ExperienceDto
{
    public int Id { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    [JsonPropertyName("about")]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AboutId { get; set; }
}

public class SkillCategoryCreateDto
{
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("about")]
    public string Description { get; set; } = string.Empty;
}

public class SkillItemCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public class ExperienceCreateDto
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    [JsonPropertyName("about")]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
