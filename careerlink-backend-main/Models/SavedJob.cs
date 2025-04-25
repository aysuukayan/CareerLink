using CareerLink.Models;
using System;

namespace CareerLinkBackend1.Models
{
    public class SavedJob
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

      
        public JobPosting Job { get; set; }
    }
}
