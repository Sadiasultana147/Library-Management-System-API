using LMS_BAL.AuthorsRepo;
using LMS_BAL.UserRepo;
using LMS_DATA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserInfo user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isRegistered = await _userRepository.RegisterUser(user);

            if (isRegistered)
                return Ok("User registered successfully");
            else
                return StatusCode(500, "User registration failed"); 
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                // Authenticate user
                var user = await _userRepository.LoginUser(loginModel.Email, loginModel.Password);

                if (user != null)
                {
                    return Ok(user); 
                }
                else
                {
                    return Unauthorized("Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the login request");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllAuthor()
        {
            var user = await _userRepository.GetAllUser();
            return Ok(user);
        }
    }

}
