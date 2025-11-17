using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Hotels;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Services;

public class HotelsService(HotelListingDbContext context) : IHotelsService
{
    public async Task<IEnumerable<GetHotelDto>> GetHotelsAsync()
    {
        var hotels = await context.Hotels
        .Include(q => q.Country)
        .Select(h => new GetHotelDto(
            h.Id,
            h.Name,
            h.Address,
            h.Rating,
            h.CountryId,
            h.Country!.Name))
        .ToListAsync();

        return hotels;
    }

    public async Task<GetHotelDto?> GetHotelAsync(int id)
    {
        var hotel = await context.Hotels
            .Where(h => h.Id == id)
            .Select(h => new GetHotelDto(
                h.Id,
                h.Name,
                h.Address,
                h.Rating,
                h.CountryId,
                h.Country!.Name))
            .FirstOrDefaultAsync();

        return hotel ?? null;
    }

    public async Task<GetHotelDto> CreateHotelAsync(CreateHotelDto createDto)
    {
        var hotel = new Hotel
        {
            Name = createDto.Name,
            Address = createDto.Address,
            Rating = createDto.Rating,
            CountryId = createDto.CountryId
        };

        context.Hotels.Add(hotel);
        await context.SaveChangesAsync();

        return new GetHotelDto(
            hotel.Id,
            hotel.Name,
            hotel.Address,
            hotel.Rating,
            hotel.CountryId,
            string.Empty
            );
    }

    public async Task UpdateHotelAsync(int id, UpdateHotelDto updateDto)
    {
        var hotel = await context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel not found");

        hotel.Name = updateDto.Name;
        hotel.Address = updateDto.Address;
        hotel.Rating = updateDto.Rating;
        hotel.CountryId = updateDto.CountryId;

        context.Entry(hotel).State = EntityState.Modified;

        context.Hotels.Update(hotel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteHotelAsync(int id)
    {
        var Hotel = await context.Hotels
            .Where(h => h.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<bool> HotelExistsAsync(int id)
    {
        return await context.Hotels.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> HotelExistsAsync(string name)
    {
        return await context.Hotels.AnyAsync(c => c.Name == name);
    }



}
