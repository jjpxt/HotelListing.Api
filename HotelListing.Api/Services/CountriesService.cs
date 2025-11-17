using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Country;
using HotelListing.Api.DTOs.Hotels;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Services
{
    public class CountriesService(HotelListingDbContext context) : ICountriesService
    {
        public async Task<IEnumerable<GetCountriesDto>> GetCountriesAsync()
        {
            return await context.Countries
                .Select(c => new GetCountriesDto(c.CountryId, c.Name, c.ShortName))
                .ToListAsync();
        }

        public async Task<GetCountryDto?> GetCountryAsync(int id)
        {
            var country = await context.Countries
            .Where(c => c.CountryId == id)
            .Select(c => new GetCountryDto(
                c.CountryId,
                c.Name,
                c.ShortName,
                c.Hotels.Select(h => new GetHotelSlimDto(
                    h.Id,
                    h.Name,
                    h.Address,
                    h.Rating
                    )).ToList()
                ))
            .FirstOrDefaultAsync();

            return country ?? null;
        }

        public async Task<GetCountryDto> CreateCountryAsync(CreateCoutryDto createDto)
        {
            var country = new Country
            {
                Name = createDto.Name,
                ShortName = createDto.ShortName
            };

            context.Countries.Add(country);
            await context.SaveChangesAsync();
            return new GetCountryDto(
                country.CountryId,
                country.Name,
                country.ShortName,
                []
                );
        }
        
        public async Task UpdateCountryAsync(int id, UpdateCountryDto updateDto)
        {
            var country = await context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");

            country.Name = updateDto.Name;
            country.ShortName = updateDto.ShortName;
            context.Countries.Update(country);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCountryAsync(int id)
        {
            var country = await context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");

            context.Countries.Remove(country);
            await context.SaveChangesAsync();
        }

        public async Task<bool> CountryExistsAsync(int id)
        {
            return await context.Countries.AnyAsync(c => c.CountryId == id);
        }

        public async Task<bool> CountryExistsAsync(string name)
        {
            return await context.Countries.AnyAsync(c => c.Name == name);
        }
    }
}
