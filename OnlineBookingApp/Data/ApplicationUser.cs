using Microsoft.AspNetCore.Identity;

namespace OnlineBookingApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
