using HappyInventory.Models.DTOs.User;
using HappyInventory.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HappyInventory.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.LoginAsync(loginDto.Email, loginDto.Password);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("create-new-user")]
        [Authorize(Roles = "Admin,Management")]
        public async Task<IActionResult> CreateNewUser([FromBody] NewUserDto newUserDto)
        {
            var result = await _userService.CreateNewUser(newUserDto);

            return result.StatusCode == HttpStatusCode.OK
               ? Ok(result)
               : StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("get-user-by-id/{id}")]
        [Authorize(Roles = "Admin,Management")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            return result.StatusCode == HttpStatusCode.OK
              ? Ok(result)
              : StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("get-all-users")]
        [Authorize(Roles = "Admin,Management")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            return result.StatusCode == HttpStatusCode.OK
              ? Ok(result)
              : StatusCode((int)result.StatusCode, result);
        }
        //[HttpPut("UpdateUser/{id}")]
        //[Authorize(Roles = "Admin")] 
        //public async Task<IActionResult> UpdateUser(int id, [FromBody] NewUserDto newUserDto)
        //{
        //    var result = await _userService.ed(id, updateUserDto);
        //    if (result.Success)
        //        return HandleSuccess(result.Data, "User updated successfully", HttpStatusCode.OK);

        //    return HandleError<string>("Error updating user", HttpStatusCode.BadRequest);
        //}
    }
}

