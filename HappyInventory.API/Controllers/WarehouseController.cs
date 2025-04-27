using HappyInventory.Helpers.Enum;
using HappyInventory.Models.DTOs.Warehouse;
using HappyInventory.Services.WarehouseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HappyInventory.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }
        [HttpPost("create-Warehouse")]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseDto warehouseDto)
        {
            var result = await _warehouseService.CreateWarehouse(warehouseDto);
            return result.StatusCode == HttpStatusCode.OK
              ? Ok(result)
              : StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("get-all-warhouses")]
        [Authorize(Roles = $"{nameof(UserRole.Admin)},{nameof(UserRole.Management)}")]
        public async Task<IActionResult> GetAllWarehouses(int pageIndex, int pageSize)
        {
            var result = await _warehouseService.GetAllWarehousesAsync(pageIndex, pageSize);

            return result.StatusCode == HttpStatusCode.OK
              ? Ok(result)
              : StatusCode((int)result.StatusCode, result);
        }

    }
}
