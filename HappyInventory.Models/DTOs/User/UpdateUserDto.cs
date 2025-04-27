using HappyInventory.Helpers.Enum;

namespace HappyInventory.Models.DTOs.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public UserRole Role { get; set; }

    }
}
