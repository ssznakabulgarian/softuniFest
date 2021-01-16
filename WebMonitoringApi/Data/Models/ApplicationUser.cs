using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WebMonitoringApi.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Urls = new HashSet<Url>();
        }

        public virtual ICollection<Url> Urls { get; set; }
    }
}
