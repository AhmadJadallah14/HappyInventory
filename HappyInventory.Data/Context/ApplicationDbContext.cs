using HappyInventory.Models.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace HappyInventory.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region Entities
        public DbSet<User> Users { get; set; }
        #endregion
    }
}