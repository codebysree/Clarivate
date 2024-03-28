using Clarivate.Models.Entity;
using Clarivate.Models.ViewModel;
using Clarivate.Services;
using Microsoft.AspNetCore.Mvc;

namespace Clarivate.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRandomUserService _randomUserService;
        public UserController(IRandomUserService randomUserService)
        {
            _randomUserService = randomUserService;
        }
        public string GetString()
        {
            return "hello world";
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserModel userModel)
        {
            return new JsonResult(await _randomUserService.GetRandomUser());
        }
    }
}
