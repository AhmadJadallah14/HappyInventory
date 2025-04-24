using HappyInventory.Helpers.Enum;

namespace HappyInventory.Models.Models.Identity
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public UserRole Role { get; set; }
    }
}
