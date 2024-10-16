using Car_rental.Entities;
using Car_rental.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReturnDetailController : Controller
    {
            private readonly IReturnDetailRepository _returnDetailRepository;

            public ReturnDetailController(IReturnDetailRepository returnDetailRepository)
            {
                _returnDetailRepository = returnDetailRepository;
            }

            [HttpPost]
            public IActionResult AddReturnDetail([FromForm] ReturnDetail returnDetail)
            {
                if (returnDetail == null)
                {
                    return BadRequest("Return detail cannot be null.");
                }

                _returnDetailRepository.AddReturnDetail(returnDetail);
                return CreatedAtAction(nameof(GetReturnDetailById), new { id = returnDetail.ReturnId }, returnDetail);
            }

            [HttpGet("{id}")]
            public IActionResult GetReturnDetailById(string id)
            {
                var returnDetail = _returnDetailRepository.GetReturnDetailById(id);
                if (returnDetail == null)
                {
                    return NotFound("Return detail not found.");
                }

                return Ok(returnDetail);
            }

            [HttpGet]
            public IActionResult GetAllReturnDetails()
            {
                var returnDetails = _returnDetailRepository.GetAllReturnDetails();
                return Ok(returnDetails);
            }

            [HttpPut("{id}")]
            public IActionResult UpdateReturnDetail(string id, [FromForm] ReturnDetail returnDetail)
            {
                if (id != returnDetail.ReturnId)
                {
                    return BadRequest("Return ID mismatch.");
                }

                var existingReturnDetail = _returnDetailRepository.GetReturnDetailById(id);
                if (existingReturnDetail == null)
                {
                    return NotFound("Return detail not found.");
                }

                _returnDetailRepository.UpdateReturnDetail(returnDetail);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteReturnDetail(string id)
            {
                var existingReturnDetail = _returnDetailRepository.GetReturnDetailById(id);
                if (existingReturnDetail == null)
                {
                    return NotFound("Return detail not found.");
                }

                _returnDetailRepository.DeleteReturnDetail(id);
                return NoContent();
            }
    }
}
