using Car_rental.Entities;
using Car_rental.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Car_rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnDetailController : ControllerBase
    {
        private readonly IReturnDetailRepository _returnDetailRepository;

        public ReturnDetailController(IReturnDetailRepository returnDetailRepository)
        {
            _returnDetailRepository = returnDetailRepository;
        }

        [HttpGet]
        public ActionResult<List<ReturnDetail>> GetAllReturnDetails()
        {
            var returnDetails = _returnDetailRepository.GetAllReturnDetails();
            return Ok(returnDetails);
        }

        [HttpGet("{returnId}")]
        public ActionResult<ReturnDetail> GetReturnDetail(string returnId)
        {
            var returnDetail = _returnDetailRepository.GetReturnDetailById(returnId);
            if (returnDetail == null)
            {
                return NotFound();
            }
            return Ok(returnDetail);
        }

        [HttpPost("Add-ReturnDetail")]
        public ActionResult AddReturnDetail([FromForm] ReturnDetail returnDetail)
        {
            if (returnDetail == null)
            {
                return BadRequest("Return detail is null.");
            }

            _returnDetailRepository.AddReturnDetail(returnDetail);
            return CreatedAtAction(nameof(GetReturnDetail), new { returnId = returnDetail.ReturnId }, returnDetail);
        }

        [HttpPut("{returnId}")]
        public ActionResult UpdateReturnDetail(string returnId, [FromBody] ReturnDetail returnDetail)
        {
            if (returnDetail == null || returnId != returnDetail.ReturnId)
            {
                return BadRequest("Return detail is null or ID mismatch.");
            }

            var existingDetail = _returnDetailRepository.GetReturnDetailById(returnId);
            if (existingDetail == null)
            {
                return NotFound();
            }

            _returnDetailRepository.UpdateReturnDetail(returnDetail);
            return NoContent();
        }

        [HttpDelete("{returnId}")]
        public ActionResult DeleteReturnDetail(string returnId)
        {
            var existingDetail = _returnDetailRepository.GetReturnDetailById(returnId);
            if (existingDetail == null)
            {
                return NotFound();
            }

            _returnDetailRepository.DeleteReturnDetail(returnId);
            return NoContent();
        }
    }
}
