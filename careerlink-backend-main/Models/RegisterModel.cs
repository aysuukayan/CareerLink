namespace CareerLinkBackend1.Models
{
    public class RegisterModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsCompany { get; set; } 
        
        public string? CompanyName { get; set; }
        public string? Website { get; set; }
        public string? Description { get; set; }

    }

}
