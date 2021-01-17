using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebMonitoringApi.Common;
using WebMonitoringApi.Data.Models;

namespace WebMonitoringApi.Services
{
    public interface IUserService
    {
        Task<TokenResponse> Authenticate(string username, string password);
        Task<IdentityResult> Create(string username, string password, string email);

        Task<IEnumerable<IdentityResult>> Update(
            string currentUserName,
            string newUserName,
            string currentPassword,
            string newPassword,
            string currentEmail,
            string newEmail
        );

        Task<ApplicationUser> Get(string id);
        Task<IdentityResult> Delete(string id);
    }
}
