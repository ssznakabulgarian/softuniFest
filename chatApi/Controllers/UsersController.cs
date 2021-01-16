using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMonitoringApi.InputModels;
using WebMonitoringApi.Services;

namespace WebMonitoringApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> SignIn(SignInUserInputModel input)
        {
            var result = await _userService.Authenticate(input.UserName, input.Password);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> SingUp(SignUpUserInputModel input)
        {
            var result = await _userService.Create(input.UserName, input.Password, input.Email);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}