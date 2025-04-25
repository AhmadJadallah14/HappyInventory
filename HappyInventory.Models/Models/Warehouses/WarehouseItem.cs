using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyInventory.Models.Models.Warehouses
{
    public  class WarehouseItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string SKUCode { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal MSRPPrice { get; set; }
        public int WarehouseId { get; set; }  
        public Warehouse Warehouse { get; set; } 
    }
}
