using Microsoft.AspNetCore.Identity;

namespace CareerLink.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty; 
        public bool IsCompany { get; set; } // true = Şirket, false = Normal Kullanıcı

        
        public string? CompanyName { get; set; } 
        public string? Website { get; set; } 
        public string? Description { get; set; } 

    }
}
