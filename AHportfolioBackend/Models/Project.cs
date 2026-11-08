namespace PortfolioAPI.Models;

public class Project
{
    public int Id { get; set; }
    public string TitleAr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string DescAr { get; set; } = string.Empty;
    public string DescEn { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? GithubRepoUrl { get; set; }
    public string? LiveDemoUrl { get; set; }
    public string Tags { get; set; } = string.Empty; // comma-separated
    public int Order { get; set; }
}
