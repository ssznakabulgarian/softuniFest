using chatApi.Data.Models.Input_Models;
using chatApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace chatApi.Controllers
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
            var result = await _userService.Create(input.UserName, input.Password, input.Email, null);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}