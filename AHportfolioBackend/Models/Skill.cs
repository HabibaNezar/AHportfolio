namespace PortfolioAPI.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int ProficiencyLevel { get; set; } // 1-5
    }

}
