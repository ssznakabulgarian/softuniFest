using System.Net.Http;
using System.Threading.Tasks;
using WebMonitoringApi.Common;
using System.Collections.Generic;
using WebMonitoringApi.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace WebMonitoringApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<LoginResult> Authenticate(string username, string password)
        {
            var httpClient = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "client_id", "api" },
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
    }
}