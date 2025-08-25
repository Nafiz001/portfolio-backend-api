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
        public DbSet<Education> Education { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for projects - will be restored on each deployment
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Title = "Payoo app",
                    Description = "works like bkash",
                    Image = "https://via.placeholder.com/400x250/333/fff?text=Payoo+App",
                    Technologies = "HTML CSS JS",
                    GitHubUrl = "https://github.com/Nafiz001/Payoo",
                    LiveUrl = "https://nafiz001.github.io/Payoo/index.html",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // Seed data for education
            modelBuilder.Entity<Education>().HasData(
                new Education
                {
                    Id = 1,
                    Degree = "Secondary School Certificate (SSC)",
                    Institution = "Your High School Name",
                    Duration = "2018 - 2020",
                    Description = "Completed secondary education with focus on science subjects.",
                    Grade = "GPA 5.00",
                    Location = "Your City, Country",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Education
                {
                    Id = 2,
                    Degree = "Higher Secondary Certificate (HSC)",
                    Institution = "Your College Name",
                    Duration = "2020 - 2022",
                    Description = "Completed higher secondary education in Science group with Mathematics, Physics, Chemistry, and Biology.",
                    Grade = "GPA 5.00",
                    Location = "Your City, Country",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Education
                {
                    Id = 3,
                    Degree = "Bachelor of Science in Computer Science and Engineering",
                    Institution = "Your University Name",
                    Duration = "2022 - Present",
                    Description = "Currently pursuing BSc in CSE with focus on software development, algorithms, and system design.",
                    Grade = "CGPA 3.8/4.0",
                    Location = "Your City, Country",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
