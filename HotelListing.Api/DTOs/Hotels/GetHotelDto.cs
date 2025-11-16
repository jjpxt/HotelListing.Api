namespace HotelListing.Api.DTOs.Hotels;

public record GetHotelDto(
    int Id,
    string Name,
    string Address,
    double Rating,
    string Country
    );
