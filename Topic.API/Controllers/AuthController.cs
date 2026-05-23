using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Topic.Contracts;
using Topic.Models.Identity;
using Topic.Models;
using Topic.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Topic.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ApiResponse _response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequestDTO model)
        {
            var loginRespone = await _authService.Login(model);

            if (loginRespone == null)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                _response.Message = "Username or password is incorrect !";

                return StatusCode(_response.StatusCode, _response);
            }

            _response.Result = loginRespone;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "User logged in successfully !";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegistrationRequestDTO model)
        {
            await _authService.Register(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "User registered successfully !";

            return StatusCode(_response.StatusCode, _response);
        }


        [HttpPost("registeradmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromForm] RegistrationRequestDTO model)
        {

            await _authService.RegisterAdmin(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Admin registered successfully !";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost("block")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BlockUser([FromForm] UserBlock_UnblockDTO userId)
        {

            var user = await _authService.BlockUser(userId);
            if (user == null)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                _response.Message = "User wasn't found !";

                return StatusCode(_response.StatusCode, _response);
            }

            _response.Result = user;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "User blocked successfully !";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost("unBlock")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnBlockUser([FromForm] UserBlock_UnblockDTO userId)
        {

            var user = await _authService.UnblockUser(userId);
            if (user == null)
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                _response.Message = "User wasn't found !";

                return StatusCode(_response.StatusCode, _response);
            }

            _response.Result = user;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "User unblocked successfully !";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("usersInfo")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _authService.GetAllUsers();

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpGet("GetUserByEmail")]
        [Authorize]
        public async Task<IActionResult> GetUserByEmail(string mail)
        {
            var result = await _authService.GetUsersByMail(mail);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPut("UserUpdate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser([FromForm]UserUpdate user)
        {
            var result = await _authService.UpdateUser(user);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
    }
}
