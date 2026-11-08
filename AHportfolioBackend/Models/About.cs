using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioAPI.Models;

public class About
{
    public int Id { get; set; }
    [Column("About")]
    public string Text { get; set; } = string.Empty;
    public List<SkillCategory> SkillCategories { get; set; } = new();
    public List<Experience> Experiences { get; set; } = new();
}
