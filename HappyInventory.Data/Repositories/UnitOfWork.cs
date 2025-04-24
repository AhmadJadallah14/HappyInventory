using HappyInventory.Data.Context;
using HappyInventory.Models.Models.Identity;

namespace HappyInventory.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IRepository<User> _userRepository;
        //private IRepository<Warehouse> _warehouseRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users => _userRepository 
            ??= new Repository<User>(_context);
        //public IRepository<Warehouse> Warehouses => _warehouseRepository ??= new Repository<Warehouse>(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

