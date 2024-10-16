using Car_rental.Entities;
using Car_rental.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingPaymentController : Controller
    {
            private readonly IBookingPaymentRepository _bookingPaymentRepository;

            public BookingPaymentController(IBookingPaymentRepository bookingPaymentRepository)
            {
                _bookingPaymentRepository = bookingPaymentRepository;
            }

            // GET: api/BookingPayment
            [HttpGet]
            public ActionResult<IEnumerable<BookingPayment>> GetAllBookingPayments()
            {
                var payments = _bookingPaymentRepository.GetAllBookingPayments();
                return Ok(payments);
            }

            // GET: api/BookingPayment/{id}
            [HttpGet("{id}")]
            public ActionResult<BookingPayment> GetBookingPaymentById(string id)
            {
                var payment = _bookingPaymentRepository.GetBookingPaymentById(id);
                if (payment == null)
                {
                    return NotFound();
                }
                return Ok(payment);
            }

            // POST: api/BookingPayment
            [HttpPost]
            public ActionResult<BookingPayment> CreateBookingPayment([FromForm] BookingPayment payment)
            {
                if (payment == null)
                {
                    return BadRequest();
                }
                _bookingPaymentRepository.AddBookingPayment(payment);
                return CreatedAtAction(nameof(GetBookingPaymentById), new { id = payment.PaymentId }, payment);
            }

            // PUT: api/BookingPayment/{id}
            [HttpPut("{id}")]
            public ActionResult UpdateBookingPayment(string id, [FromForm] BookingPayment payment)
            {
                if (payment == null || id != payment.PaymentId)
                {
                    return BadRequest();
                }

                var existingPayment = _bookingPaymentRepository.GetBookingPaymentById(id);
                if (existingPayment == null)
                {
                    return NotFound();
                }

                _bookingPaymentRepository.UpdateBookingPayment(payment);
                return NoContent();
            }

            // DELETE: api/BookingPayment/{id}
            [HttpDelete("{id}")]
            public ActionResult DeleteBookingPayment(string id)
            {
                var existingPayment = _bookingPaymentRepository.GetBookingPaymentById(id);
                if (existingPayment == null)
                {
                    return NotFound();
                }

                _bookingPaymentRepository.DeleteBookingPayment(id);
                return NoContent();
            }
    }
}
