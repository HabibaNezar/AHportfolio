namespace PortfolioAPI.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string AuthorRole { get; set; } = string.Empty;
        public string TextAr { get; set; } = string.Empty;
        public string TextEn { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }

    }
}
