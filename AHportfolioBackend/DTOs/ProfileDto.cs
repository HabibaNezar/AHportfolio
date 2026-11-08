namespace PortfolioAPI.DTOs
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string BioAr { get; set; } = string.Empty;
        public string BioEn { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string? ResumeUrl { get; set; }
        public string? GithubUrl { get; set; }
        public string? LinkedInUrl { get; set; }
    }
}
