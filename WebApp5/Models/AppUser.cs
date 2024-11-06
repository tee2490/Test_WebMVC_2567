using Microsoft.AspNetCore.Identity;


namespace WebApp5.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

    }
}
