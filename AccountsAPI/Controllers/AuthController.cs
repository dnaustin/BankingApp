using AccountsAPI.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public AuthController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] UserCred userCred)
        {
            string token = jwtAuthenticationManager.Authenticate(userCred.Username, userCred.PinCode);
            return token is null ? Unauthorized() : Ok(token);
        }

        public class UserCred
        {
            public string Username { get; set; }
            public int PinCode { get; set; }
        }
    }
}
