using HappyInventory.Models.DTOs.Warehouse;
using HappyInventory.Models.Response;

namespace HappyInventory.Services.WarehouseService
{
    public interface IWarehouseService
    {
        Task<ApiResponse<WarehouseDto>> CreateWarehouse(WarehouseDto warehouseDto);
    }
}