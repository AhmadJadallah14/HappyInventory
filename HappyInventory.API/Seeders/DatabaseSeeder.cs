using HappyInventory.Services.CountryService.CounrtySeeder;
using HappyInventory.Services.UserService.SeedData;

namespace HappyInventory.API.Seeders
{
    public class DatabaseSeeder
    {
        private readonly IUserSeeder _userSeeder;
        private readonly ICountrySeeder _countrySeeder;

        public DatabaseSeeder(IUserSeeder userSeeder, ICountrySeeder countrySeeder)
        {
            _userSeeder = userSeeder;
            _countrySeeder = countrySeeder;
        }

        public async Task SeedDataAsync()
        {
            await _userSeeder.SeedDefaultUserAsync();

            await _countrySeeder.SeedDefaultCountriesAsync();
        }
    }
}

