using HappyInventory.Models.DTOs.User;
using HappyInventory.Models.Enum;
using HappyInventory.Models.Response;

namespace HappyInventory.Services.UserService
{
    public interface IUserService
    {
        Task<ApiResponse<string>> CreateNewUser(NewUserDto dto);
        Task<ApiResponse<List<UserDto>>> GetAllUsersAsync();
        Task<ApiResponse<UserDto>> GetUserByIdAsync(int id);
        Task<ApiResponse<string>> LoginAsync(string email, string password);
    }
}