using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;

namespace PortfolioAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();
    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
    public DbSet<About> Abouts => Set<About>();
    public DbSet<SkillCategory> SkillCategories => Set<SkillCategory>();
    public DbSet<SkillItem> SkillItems => Set<SkillItem>();
    public DbSet<Experience> Experiences => Set<Experience>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Article>().HasIndex(a => a.Slug).IsUnique();
        modelBuilder.Entity<Project>().Property(p => p.Order).HasDefaultValue(0);

        modelBuilder.Entity<About>()
            .HasMany(a => a.SkillCategories)
            .WithOne(c => c.About)
            .HasForeignKey(c => c.AboutId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<About>()
            .HasMany(a => a.Experiences)
            .WithOne(e => e.About)
            .HasForeignKey(e => e.AboutId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SkillCategory>()
            .HasMany(c => c.Items)
            .WithOne(i => i.SkillCategory)
            .HasForeignKey(i => i.SkillCategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
