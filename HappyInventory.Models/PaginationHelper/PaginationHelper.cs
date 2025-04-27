using HappyInventory.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace HappyInventory.Models.PaginationHelper
{
    public static class PaginationHelper
    {
        public static async Task<PaginatedResponse<T>> GetPaginatedResultAsync<T>(IQueryable<T> query, int pageIndex, int pageSize)
        {
            var totalRecords = await query.CountAsync();

            var paginatedData = await query
                .Skip((pageIndex - 1) * pageSize)  
                .Take(pageSize)  
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PaginatedResponse<T>
            {
                Data = paginatedData,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = pageIndex,
                PageSize = pageSize
            };
        }
    }
}
