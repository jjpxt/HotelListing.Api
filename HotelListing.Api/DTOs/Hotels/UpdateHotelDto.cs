using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Hotels;

public class UpdateHotelDto : CreateHotelDto
{
    [Required]
    public int Id { get; set; }
}
