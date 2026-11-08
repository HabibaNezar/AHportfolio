namespace PortfolioAPI.Models;

public class SkillItem
{
    public SkillItem() { }
    public SkillItem(string name, string icon)
    {
        Name = name;
        Icon = icon;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int SkillCategoryId { get; set; }
    public SkillCategory? SkillCategory { get; set; }
}
