using System.ComponentModel.DataAnnotations;

namespace PortfolioAPI.Models
{
    public class Education
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Degree { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Institution { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Duration { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Grade { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
