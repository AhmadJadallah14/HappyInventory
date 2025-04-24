using HappyInventory.Models.Enum;

namespace HappyInventory.Models.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public UserRole Role { get; set; }
    }
}
