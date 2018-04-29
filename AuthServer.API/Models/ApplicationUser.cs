using Microsoft.AspNetCore.Identity;

namespace AuthServer.API.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser() { }
        
        public ApplicationUser(string username) : base(username) { }
    }
}