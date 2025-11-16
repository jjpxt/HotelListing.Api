using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.DTOs.Country;

public class UpdateCountryDto : CreateCoutryDto
{
    [Required]
    public int Id { get; set; }
}

