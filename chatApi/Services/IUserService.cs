using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace chatApi.Services
{
    public interface IUserService
    {
        public Task<Data.Models.SignInResult> Authenticate(string username, string password);

        public Task<IdentityResult> Create(string username, string password, string? email, string? address);
    }
}