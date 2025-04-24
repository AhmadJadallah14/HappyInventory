using HappyInventory.Data.Context;
using HappyInventory.Helpers.Helper;
using HappyInventory.Models.Enum;
using HappyInventory.Models.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace HappyInventory.Services.UserService.SeedData
{
    public class UserSeeder : IUserSeeder
    {
        private readonly ApplicationDbContext _context;

        public UserSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDefaultUserAsync()
        {

            var defaultUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals("admin@happywarehouse.com"));

            if (defaultUser == null)
            {
                // Create the default admin user
                defaultUser = new User
                {
                    FullName = "Admin User",
                    Email = "admin@happywarehouse.com",
                    IsActive = true,
                    PasswordHash = Encryption.HashPassword("P@ssw0rd"),
                    Role = UserRole.Admin
                };

                _context.Users.Add(defaultUser);
                await _context.SaveChangesAsync();
            }
        }

    }
}

