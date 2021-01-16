using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebMonitoringApi.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
            Urls = new HashSet<Url>();
        }

        public virtual ICollection<Url> Urls { get; set; }
    }
}