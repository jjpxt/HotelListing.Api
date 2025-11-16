namespace HotelListing.Api.DTOs.Hotels;

public record GetHotelsDto(
    int Id,
    string Name,
    string Address,
    double Rating,
    int CountryId
    );

public record GetHotelSlimDto(
    int Id,
    string Name,
    string Address,
    double Rating
    );