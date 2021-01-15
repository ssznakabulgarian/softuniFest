using Microsoft.AspNetCore.Identity;

namespace chatApi.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
        }

        public ApplicationUser(string username) : base(username)
        {
        }
    }
}