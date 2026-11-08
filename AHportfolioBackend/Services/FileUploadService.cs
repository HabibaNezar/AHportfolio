namespace PortfolioAPI.Services;

public interface IFileUploadService
{
    Task<string> SaveFileAsync(IFormFile file, string subfolder);
}

public class FileUploadService : IFileUploadService
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileUploadService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
    {
        var uploadsRoot = Path.Combine(_env.WebRootPath, "uploads", subfolder);
        Directory.CreateDirectory(uploadsRoot);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(uploadsRoot, fileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var request = _httpContextAccessor.HttpContext!.Request;
        return $"{request.Scheme}://{request.Host}/uploads/{subfolder}/{fileName}";
    }
}