namespace PortfolioAPI.DTOs
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string ContentAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;
        public string ExcerptAr { get; set; } = string.Empty;
        public string ExcerptEn { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
        public List<string> Tags { get; set; } = new();
        public string Status { get; set; } = string.Empty; // "Draft" or "Published"
        public DateTime? PublishedAt { get; set; }
    }
    public class ArticleCreateDto
    {
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string ContentAr { get; set; } = string.Empty;
        public string ContentEn { get; set; } = string.Empty;
        public string ExcerptAr { get; set; } = string.Empty;
        public string ExcerptEn { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
        public List<string> Tags { get; set; } = new();
        public string Status { get; set; } = "Draft";
    }
}
