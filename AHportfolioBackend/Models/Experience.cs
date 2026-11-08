namespace PortfolioAPI.Models;

public class Experience
{
    public int Id { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AboutId { get; set; }
    public About? About { get; set; }
}
