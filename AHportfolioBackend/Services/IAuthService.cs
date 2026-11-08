using PortfolioAPI.Models;

namespace PortfolioAPI.Services
{
    public interface IAuthService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
        string GenerateJwtToken(User user, out DateTime expiresAt);
    }

}
