using System.Net.Http;
using System.Threading.Tasks;
using WebMonitoringApi.Common;
using System.Collections.Generic;
using WebMonitoringApi.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebMonitoringApi.Data;

namespace WebMonitoringApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<LoginResult> Authenticate(string username, string password)
        {
            var httpClient = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "client_id", "WebMonitoringApi" },
                { "grant_type", "password" },
                { "username", username },
                { "password", password },
                { "scope", "WebMonitoringApi" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await httpClient.PostAsync("https://localhost:44340/connect/token", content);
            
            return new LoginResult
            {
                Succeeded = response.IsSuccessStatusCode,
                Jwt = await response.Content.ReadAsStringAsync()
            }; 
        }

        public async Task<IdentityResult> Create(string username, string password, string email)
        {
            return await _userManager.CreateAsync(new ApplicationUser { UserName = username, Email = email }, password);
        }

        public async Task<ApplicationUser> Update(string currentUserName, string newUserName, string currentPassword, string newPassword, string currentEmail, string newEmail)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == currentUserName);
            if (newUserName != null) await _userManager.SetUserNameAsync(user, newUserName);
            if (newPassword != null) await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (newEmail != null) await _userManager.ChangeEmailAsync(user, newEmail, await _userManager.GenerateChangeEmailTokenAsync(user, newEmail));

            return user;
        }

        public async Task<ApplicationUser> Get(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}