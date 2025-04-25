using HappyInventory.Models.DTOs.Warehouse;
using HappyInventory.Services.WarehouseService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HappyInventory.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}
