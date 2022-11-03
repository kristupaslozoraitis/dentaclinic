using Microsoft.AspNetCore.Identity;

namespace DentaClinic.Models
{
    public class User : IdentityUser
    {
        public string Email { get; set; }
    }
}
