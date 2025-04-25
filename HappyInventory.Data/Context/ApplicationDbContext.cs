using HappyInventory.Models.Models.Countries;
using HappyInventory.Models.Models.Identity;
using HappyInventory.Models.Models.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace HappyInventory.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region Entities
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseItem> warehouseItems { get; set; }
        #endregion
    }
}