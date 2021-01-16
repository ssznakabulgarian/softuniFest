using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebMonitoringApi.Data.Models;

namespace WebMonitoringApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Data.Models.SignInResult> Authenticate(string username, string password)
        {
            var httpClient = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "client_id", "api" },
                { "grant_type", "password" },
                { "username", username },
                { "password", password },
                { "scope", "chatAPI" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await httpClient.PostAsync("https://localhost:44340/connect/token", content);
            
            return new Data.Models.SignInResult
            {
                Succeeded = response.IsSuccessStatusCode,
                Jwt = await response.Content.ReadAsStringAsync()
            }; //TODO: figure out how to serialize the http request result
        }

        public async Task<IdentityResult> Create(string username, string password, string email)
        {
            return await _userManager.CreateAsync(new ApplicationUser { UserName = username, Email = email }, password);
        }
    }
}