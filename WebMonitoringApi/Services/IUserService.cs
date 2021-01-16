using System.Collections.Generic;
using System.Threading.Tasks;
using WebMonitoringApi.Common;
using Microsoft.AspNetCore.Identity;
using WebMonitoringApi.Data;
using WebMonitoringApi.Data.Models;

namespace WebMonitoringApi.Services
{
    public interface IUserService
    {
        Task<LoginResult> Authenticate(string username, string password);
        Task<IdentityResult> Create(string username, string password, string email);
        Task<IEnumerable<IdentityResult>> Update(string currentUserName, string newUserName, string currentPassword, string newPassword, string currentEmail, string newEmail);
        Task<ApplicationUser> Get(string id);
        Task<IdentityResult> Delete(string id);
    }
}