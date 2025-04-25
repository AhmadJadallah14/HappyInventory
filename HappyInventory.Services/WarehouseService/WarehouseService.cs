using AutoMapper;
using HappyInventory.Data.Repositories;
using HappyInventory.Helpers.AppConstant;
using HappyInventory.Models.DTOs.Warehouse;
using HappyInventory.Models.Models.Countries;
using HappyInventory.Models.Models.Warehouses;
using HappyInventory.Models.Response;
using HappyInventory.Services.UserService;
using System.Net;

namespace HappyInventory.Services.WarehouseService
{
    public class WarehouseService : IWarehouseService
    {

        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public WarehouseService(
            IRepository<Warehouse> warehouseRepository,
            IRepository<Country> countryRepository,
            IMapper mapper,
            IUserService userService)
        {
            _warehouseRepository = warehouseRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ApiResponse<WarehouseDto>> CreateWarehouse(WarehouseDto warehouseDto)
        {
            var country = await _countryRepository.GetByIdAsync(warehouseDto.CountryId);
            if (country is null)
                return new ApiResponse<WarehouseDto>(false, null, ResponseMessages.InvalidCountry,
                    null, HttpStatusCode.BadRequest);

            var warehouse = _mapper.Map<Warehouse>(warehouseDto);
            string loggedInUserName = _userService.GetCurrentUserFullName();
            warehouse.CreatedBy = loggedInUserName;
            warehouse.CreatedOn = DateTime.UtcNow;
            warehouse.UpdatedBy = loggedInUserName;
            warehouse.UpdateOn = DateTime.UtcNow;
            await _warehouseRepository.AddAsync(warehouse);
            await _warehouseRepository.SaveAsync();

            return new ApiResponse<WarehouseDto>(true, _mapper.Map<WarehouseDto>(warehouse),
                ResponseMessages.WarehouseCreatedSuccessfully, null, HttpStatusCode.Created);
        }

    }
}
