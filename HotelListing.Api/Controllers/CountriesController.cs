using HotelListing.Api.Data;
using HotelListing.Api.DTOs.Country;
using HotelListing.Api.DTOs.Hotels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly HotelListingDbContext _context;

    public CountriesController(HotelListingDbContext context)
    {
        _context = context;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
    {
        var countries = await _context.Countries
            .Select(c => new GetCountriesDto(
                c.CountryId,
                c.Name,
                c.ShortName
                ))
            .ToListAsync();

        return Ok(countries);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountry(int id)
    {
        var country = await _context.Countries
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

        if (country == null)
        {
            return NotFound();
        }

        return Ok(country);
    }

    // PUT: api/Countries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest();
        }

        var country = await _context.Countries.FindAsync(id);

        if (country is null) return NotFound();

        country.Name = updateDto.Name;
        country.ShortName = updateDto.ShortName;

        _context.Entry(country).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CountryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Countries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GetCountryDto>> PostCountry(CreateCoutryDto createDto)
    {
        var country = new Country
        {
            Name = createDto.Name,
            ShortName = createDto.ShortName
        };

        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        var resultDto = new GetCountryDto(
            country.CountryId,
            country.Name,
            country.ShortName,
            []
            );

        return CreatedAtAction("GetCountry", new { id = country.CountryId }, country);
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CountryExists(int id)
    {
        return _context.Countries.Any(e => e.CountryId == id);
    }
}
