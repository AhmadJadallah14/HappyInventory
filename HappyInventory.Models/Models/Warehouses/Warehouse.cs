using HappyInventory.Models.BaseEntity;
using HappyInventory.Models.Models.Countries;

namespace HappyInventory.Models.Models.Warehouses
{
    public class Warehouse : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }  
        public Country Country { get; set; }

        public List<WarehouseItem> Items { get; set; } = new(); 

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateOn { get; set; }
    }
}
