using HotelListing.Api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {

        private static List<Hotel> hotels = new List<Hotel>
        {
            new Hotel { Id = 1, Name = "Grand Plaza", Adress = "123 Main st", Rating = 4.6},
            new Hotel { Id = 2, Name = "Best View Hotel", Adress = "1 Boulevard st", Rating = 3.9},
            new Hotel { Id = 3, Name = "Copacabana Palace", Adress = "Atlantica Ave", Rating = 4.0}
        };

        // GET: api/<HotelsController>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get()
        {
            return Ok(hotels);
        }

        // GET api/<HotelsController>/5
        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            var hotel = hotels.FirstOrDefault(x => x.Id == id);

            if (hotel is null) return NotFound();

            return Ok(hotel);
        }

        // POST api/<HotelsController>
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel newHotel)
        {
            if (hotels.Any(x => x.Id == newHotel.Id))
            {
                return BadRequest("Hotel with this Id already exists");
            }

            hotels.Add(newHotel);
            return CreatedAtAction(nameof(Get), new { Id = newHotel.Id }, newHotel);
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel updatedHotel)
        {
            var existingHotel = hotels.FirstOrDefault(x => x.Id == id);

            if (existingHotel == null) return NotFound();

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Adress = updatedHotel.Adress;
            existingHotel.Rating = updatedHotel.Rating;

            return NoContent();
        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(x => x.Id == id);

            if (hotel is null) return NotFound(new { message = "Hotel not found" });

            hotels.Remove(hotel);
            return NoContent();
        }
    }
}
