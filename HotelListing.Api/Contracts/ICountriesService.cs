using HotelListing.Api.DTOs.Country;

namespace HotelListing.Api.Contracts
{
    public interface ICountriesService
    {
        Task<bool> CountryExistsAsync(int id);
        Task<bool> CountryExistsAsync(string name);
        Task<GetCountryDto> CreateCountryAsync(CreateCoutryDto createDto);
        Task DeleteCountryAsync(int id);
        Task<IEnumerable<GetCountriesDto>> GetCountriesAsync();
        Task<GetCountryDto?> GetCountryAsync(int id);
        Task UpdateCountryAsync(int id, UpdateCountryDto updateDto);
    }
}