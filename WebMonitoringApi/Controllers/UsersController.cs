using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMonitoringApi.Services;
using WebMonitoringApi.InputModels;

namespace WebMonitoringApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginInputModel input)
        {
            var result = await _userService.Authenticate(input.UserName, input.Password);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInputModel input)
        {
            var result = await _userService.Create(input.UserName, input.Password, input.Email);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}