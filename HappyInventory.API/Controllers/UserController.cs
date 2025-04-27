using HappyInventory.Helpers.Enum;
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
            var result = await _userService.LoginAsync(loginDto);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : StatusCode((int)result.StatusCode, result);
        }
        [HttpPost("create-new-user")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Management)}")]
        public async Task<IActionResult> CreateNewUser([FromBody] NewUserDto newUserDto)
        {
            var result = await _userService.CreateNewUser(newUserDto);

            return result.StatusCode == HttpStatusCode.OK
               ? Ok(result)
               : StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("get-user-by-id/{id}")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Management)}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            return result.StatusCode == HttpStatusCode.OK
              ? Ok(result)
              : StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("get-all-users")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Management)}")]
        public async Task<IActionResult> GetAllUsers(int pageIndex ,  int pageSize)
        {
            var result = await _userService.GetAllUsersAsync(pageIndex, pageSize);

            return result.StatusCode == HttpStatusCode.OK
              ? Ok(result)
              : StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("edit-user")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Management)}")]
        public async Task<IActionResult> EditUser([FromBody] UpdateUserDto updateUserDto)
        {
            var result = await _userService.EditUser(updateUserDto);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("delete-user/{userId}")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Management)}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await _userService.DeleteUser(userId);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : StatusCode((int)result.StatusCode, result);
        }
        [HttpPatch("change-password")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Management)}")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var result = await _userService.ChangePassword(dto);

            return result.StatusCode == HttpStatusCode.OK
                ? Ok(result)
                : StatusCode((int)result.StatusCode, result);
        }


    }
}

