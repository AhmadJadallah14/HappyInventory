using AutoMapper;
using HappyInventory.Data.Context;
using HappyInventory.Data.Repositories;
using HappyInventory.Helpers.AppConstant;
using HappyInventory.Models.DTOs.Warehouse;
using HappyInventory.Models.Models.Countries;
using HappyInventory.Models.Models.Warehouses;
using HappyInventory.Models.PaginationHelper;
using HappyInventory.Models.Response;
using HappyInventory.Services.UserService;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HappyInventory.Services.WarehouseService
{
    public class WarehouseService : IWarehouseService
    {

        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _Context;

        public WarehouseService(
            IRepository<Warehouse> warehouseRepository,
            IRepository<Country> countryRepository,
            IMapper mapper,
            IUserService userService,
            ApplicationDbContext Context
            )
        {
            _warehouseRepository = warehouseRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
            _userService = userService;
            _Context = Context;
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
        public async Task<ApiResponse<PaginatedResponse<WarehouseDto>>> GetAllWarehousesAsync(int pageIndex, int pageSize)
        {
            var query = _Context.Warehouses.AsQueryable()
                .Include(l => l.Country)
                .OrderByDescending(w => w.Id);

            var paginatedResult = await PaginationHelper.GetPaginatedResultAsync(query, pageIndex, pageSize);

            var warehouseDtos = _mapper.Map<List<WarehouseDto>>(paginatedResult.Data);

            return new ApiResponse<PaginatedResponse<WarehouseDto>>(
                true,
                new PaginatedResponse<WarehouseDto>
                {
                    Data = warehouseDtos,
                    TotalRecords = paginatedResult.TotalRecords,
                    TotalPages = paginatedResult.TotalPages,
                    CurrentPage = paginatedResult.CurrentPage,
                    PageSize = paginatedResult.PageSize
                },
                ResponseMessages.SuccessWarehouseFetched,
                null,
                HttpStatusCode.OK
            );
        }

    }
}

