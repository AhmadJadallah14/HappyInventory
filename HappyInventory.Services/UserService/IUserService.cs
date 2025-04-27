using HappyInventory.Models.DTOs.User;
using HappyInventory.Models.Response;

namespace HappyInventory.Services.UserService
{
    public interface IUserService
    {
        Task<ApiResponse<string>> CreateNewUser(NewUserDto dto);
        Task<ApiResponse<PaginatedResponse<UserDto>>> GetAllUsersAsync(int pageIndex, int pageSize);
        Task<ApiResponse<UserDto>> GetUserByIdAsync(int id);
        Task<ApiResponse<string>> LoginAsync(LoginDto loginDto);
        Task<ApiResponse<string>> EditUser(UpdateUserDto dto);
        Task<ApiResponse<string>> DeleteUser(int userId);
        Task<ApiResponse<string>> ChangePassword(ChangePasswordDto dto);
        string GetCurrentUserEmail();
        string GetCurrentUserFullName();
    }
}