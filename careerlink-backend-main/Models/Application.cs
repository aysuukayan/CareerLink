using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerLink.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int JobPostingId { get; set; }

        [ForeignKey("JobPostingId")]
        public JobPosting JobPosting { get; set; }

        [Required]
        public string ApplicantId { get; set; }

        [ForeignKey("ApplicantId")]
        public ApplicationUser Applicant { get; set; }

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
    }
}
