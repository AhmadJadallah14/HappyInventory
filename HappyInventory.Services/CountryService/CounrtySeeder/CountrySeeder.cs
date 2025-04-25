using HappyInventory.Data.Context;
using HappyInventory.Models.Models.Countries;
using Microsoft.EntityFrameworkCore;

namespace HappyInventory.Services.CountryService.CounrtySeeder
{
    public class CountrySeeder : ICountrySeeder
    {
        private readonly ApplicationDbContext _context;

        public CountrySeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedDefaultCountriesAsync()
        {
            if (await _context.Countries.AnyAsync())
                return;

            await _context.Countries.AddRangeAsync(new List<Country>
            {
                new() { Name = "Jordan" },
                new() { Name = "United Arab Emirates" }
            });

            await _context.SaveChangesAsync();
        }
    }
}

