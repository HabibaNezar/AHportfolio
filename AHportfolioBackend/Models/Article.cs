namespace PortfolioAPI.Models;

public enum ArticleStatus { Draft, Published }

public class Article
{
    public int Id { get; set; }
    public string TitleAr { get; set; } = string.Empty;
    public string TitleEn { get; set; } = string.Empty;
    public string ContentAr { get; set; } = string.Empty; // rich text / markdown
    public string ContentEn { get; set; } = string.Empty;
    public string ExcerptAr { get; set; } = string.Empty;
    public string ExcerptEn { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public string Tags { get; set; } = string.Empty;
    public ArticleStatus Status { get; set; } = ArticleStatus.Draft;
    public DateTime? PublishedAt { get; set; }
}
