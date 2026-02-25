using Microsoft.AspNetCore.Identity;

namespace MediaLabDapper.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}