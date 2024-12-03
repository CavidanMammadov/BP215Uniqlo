using Microsoft.AspNetCore.Identity;

namespace BP215Uniqlo.Models
{
    public class User:IdentityUser
    {
        public string FullName { get; set; }
        public string ProfilImageUrl { get; set; }
    }
}
