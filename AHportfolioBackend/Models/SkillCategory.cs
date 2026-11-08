namespace PortfolioAPI.Models;

public class SkillCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // languages, libraries, frameworks, tools, others
    public string Description { get; set; } = string.Empty;
    public int AboutId { get; set; }
    public About? About { get; set; }
    public List<SkillItem> Items { get; set; } = new();
}
