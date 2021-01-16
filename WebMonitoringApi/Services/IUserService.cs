using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebMonitoringApi.Services
{
    public interface IUserService
    {
        public Task<Data.Models.SignInResult> Authenticate(string username, string password);

        public Task<IdentityResult> Create(string username, string password, string email);
    }
}