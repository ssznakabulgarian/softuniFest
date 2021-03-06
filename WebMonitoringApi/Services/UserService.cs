﻿using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using WebMonitoringApi.Common;
using WebMonitoringApi.Data;
using WebMonitoringApi.Data.Models;

namespace WebMonitoringApi.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<TokenResponse> Authenticate(string username, string password)
        {
            var httpClient = new HttpClient();

            var values = new Dictionary<string, string>
            {
                {"client_id", "WebMonitoringApi"},
                {"grant_type", "password"},
                {"username", username},
                {"password", password},
                {"scope", "WebMonitoringApi"}
            };

            var content = new FormUrlEncodedContent(values);

            return await new HttpClient().RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = "https://localhost:44340/connect/token",

                ClientId = "WebMonitoringApi",
                Scope = "WebMonitoringApi",

                UserName = username,
                Password = password
            });
        }

        public async Task<IdentityResult> Create(string username, string password, string email)
        {
            return await _userManager.CreateAsync(new ApplicationUser {UserName = username, Email = email}, password);
        }

        public async Task<IEnumerable<IdentityResult>> Update(
            string currentUserName,
            string newUserName,
            string currentPassword,
            string newPassword,
            string currentEmail,
            string newEmail
        )
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == currentUserName);
            var result = new List<IdentityResult>();
            if (user == null)
                return new[]
                {
                    IdentityResult.Failed(new IdentityError
                    {
                        Code = "UserNotFound", Description = "User could not be found"
                    })
                };
            if (newUserName != null) result.Add(await _userManager.SetUserNameAsync(user, newUserName));
            if (newPassword != null) result.Add(await _userManager.ChangePasswordAsync(user, currentPassword, newPassword));
            if (newEmail != null) result.Add(await _userManager.ChangeEmailAsync(user, newEmail, await _userManager.GenerateChangeEmailTokenAsync(user, newEmail)));

            return result;
        }

        public async Task<ApplicationUser> Get(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IdentityResult> Delete(string id)
        {
            return await _userManager.DeleteAsync(await Get(id));
        }
    }
}
