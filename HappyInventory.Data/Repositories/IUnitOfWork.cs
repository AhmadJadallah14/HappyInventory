using HappyInventory.Models.Models.Identity;

namespace HappyInventory.Data.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        //IRepository<Warehouse> Warehouses { get; }
        Task SaveAsync();
    }
}
