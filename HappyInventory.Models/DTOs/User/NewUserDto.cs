using HappyInventory.Models.Enum;

namespace HappyInventory.Models.DTOs.User
{
    public class NewUserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public UserRole Role { get; set; }

    }
}
