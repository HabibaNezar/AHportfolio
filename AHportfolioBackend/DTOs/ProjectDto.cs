namespace PortfolioAPI.DTOs;

public class ProjectDto
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
    public List<string> Tags { get; set; } = new();
    public int Order { get; set; }
}

public class ProjectCreateDto
{
    public string TitleAr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string DescAr { get; set; } = string.Empty;
    public string DescEn { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? GithubRepoUrl { get; set; }
    public string? LiveDemoUrl { get; set; }
    public List<string> Tags { get; set; } = new();
    public int Order { get; set; }
}

public class UpdateProjectDto
{
    public string TitleAr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string DescAr { get; set; } = string.Empty;
    public string DescEn { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? GithubRepoUrl { get; set; }
    public string? LiveDemoUrl { get; set; }
    public List<string> Tags { get; set; } = new();
    public int Order { get; set; }
}