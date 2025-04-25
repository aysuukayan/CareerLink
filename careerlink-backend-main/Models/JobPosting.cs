using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLink.Models
{
    public class JobPosting
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [Required]
        public string Location { get; set; }

        public decimal? Salary { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string EmployerId { get; set; } 
        [ForeignKey("EmployerId")]
        public ApplicationUser Employer { get; set; } 
    }
}
