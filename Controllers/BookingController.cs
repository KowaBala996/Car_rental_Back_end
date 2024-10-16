using Car_rental.Entities;
using Car_rental.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {

            private readonly IBookingRepository _bookingRepository;

            public BookingController(IBookingRepository bookingRepository)
            {
                _bookingRepository = bookingRepository;
            }

            [HttpPost]
            public ActionResult<Booking> AddBooking([FromForm] Booking booking)
            {
                if (booking == null)
                {
                    return BadRequest("Invalid booking data.");
                }

                _bookingRepository.AddBooking(booking);
                return CreatedAtAction(nameof(GetBookingById), new { id = booking.BookingId }, booking);
            }

            [HttpGet]
            public ActionResult<IEnumerable<Booking>> GetAllBookings()
            {
                var bookings = _bookingRepository.GetAllBookings();
                return Ok(bookings);
            }

            [HttpGet("{id}")]
            public ActionResult<Booking> GetBookingById(string id)
            {
                var booking = _bookingRepository.GetBookingById(id);
                if (booking == null)
                {
                    return NotFound("Booking not found.");
                }
                return Ok(booking);
            }

            [HttpPut("{id}")]
            public ActionResult UpdateBooking(string id, [FromForm] Booking booking)
            {
                if (id != booking.BookingId)
                {
                    return BadRequest("Booking ID mismatch.");
                }

                var existingBooking = _bookingRepository.GetBookingById(id);
                if (existingBooking == null)
                {
                    return NotFound("Booking not found.");
                }

                _bookingRepository.UpdateBooking(booking);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public ActionResult DeleteBooking(string id)
            {
                var existingBooking = _bookingRepository.GetBookingById(id);
                if (existingBooking == null)
                {
                    return NotFound("Booking not found.");
                }

                _bookingRepository.DeleteBooking(id);
                return NoContent();
            }
    }
}
