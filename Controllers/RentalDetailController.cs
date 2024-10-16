using Car_rental.Entities;
using Car_rental.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalDetailController : Controller
    {
            private readonly IRentalDetailRepository _rentalDetailRepository;

            public RentalDetailController(IRentalDetailRepository rentalDetailRepository)
            {
                _rentalDetailRepository = rentalDetailRepository;
            }

            [HttpPost]
            public IActionResult AddRentalDetail([FromForm] RentalDetail rentalDetail)
            {
                if (rentalDetail == null)
                {
                    return BadRequest("Rental detail is null.");
                }

                _rentalDetailRepository.AddRentalDetail(rentalDetail);
                return CreatedAtAction(nameof(GetRentalDetailById), new { id = rentalDetail.RentalId }, rentalDetail);
            }

            [HttpGet("{id}")]
            public IActionResult GetRentalDetailById(string id)
            {
                var rentalDetail = _rentalDetailRepository.GetRentalDetailById(id);
                if (rentalDetail == null)
                {
                    return NotFound("Rental detail not found.");
                }
                return Ok(rentalDetail);
            }

            [HttpGet]
            public IActionResult GetAllRentalDetails()
            {
                var rentalDetails = _rentalDetailRepository.GetAllRentalDetails();
                return Ok(rentalDetails);
            }

            [HttpPut("{id}")]
            public IActionResult UpdateRentalDetail(string id, [FromForm] RentalDetail rentalDetail)
            {
                if (rentalDetail == null || rentalDetail.RentalId != id)
                {
                    return BadRequest("Rental detail is null or IDs do not match.");
                }

                var existingRentalDetail = _rentalDetailRepository.GetRentalDetailById(id);
                if (existingRentalDetail == null)
                {
                    return NotFound("Rental detail not found.");
                }

                _rentalDetailRepository.UpdateRentalDetail(rentalDetail);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteRentalDetail(string id)
            {
                var existingRentalDetail = _rentalDetailRepository.GetRentalDetailById(id);
                if (existingRentalDetail == null)
                {
                    return NotFound("Rental detail not found.");
                }

                _rentalDetailRepository.DeleteRentalDetail(id);
                return NoContent();
            }
    }
}
