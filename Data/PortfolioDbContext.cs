using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Models;

namespace PortfolioAPI.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Title = "E-Commerce Platform",
                    Description = "Full-stack e-commerce solution with payment integration and admin dashboard.",
                    Image = "https://via.placeholder.com/400x250/333/00ff88?text=E-Commerce+Platform",
                    Technologies = "React, Node.js, MongoDB, Stripe",
                    GitHubUrl = "https://github.com/username/ecommerce",
                    LiveUrl = "https://example.com/ecommerce",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Project
                {
                    Id = 2,
                    Title = "Task Management App",
                    Description = "Collaborative task management application with real-time updates.",
                    Image = "https://via.placeholder.com/400x250/333/00a8ff?text=Task+Manager",
                    Technologies = "Vue.js, Express.js, Socket.io, PostgreSQL",
                    GitHubUrl = "https://github.com/username/taskmanager",
                    LiveUrl = "https://example.com/taskmanager",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Project
                {
                    Id = 3,
                    Title = "Portfolio Website",
                    Description = "Responsive portfolio website with admin panel and content management.",
                    Image = "https://via.placeholder.com/400x250/333/ff6b6b?text=Portfolio+Site",
                    Technologies = "HTML5, CSS3, JavaScript, ASP.NET",
                    GitHubUrl = "https://github.com/username/portfolio",
                    LiveUrl = "https://example.com/portfolio",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
