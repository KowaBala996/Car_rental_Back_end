using Car_rental.IRepository;
using Car_rental.Models.RequestModel;
using Car_rental.Models.ResposeModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Car_rental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManagerController(IManagerRepository managerRepository, IWebHostEnvironment webHostEnvironment)
        {
            _managerRepository = managerRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/manager/get-all-cars
        [HttpGet("get-all-cars")]
        public IActionResult GetAllCars()
        {
            var carList = _managerRepository.GetAllCars();
            return Ok(carList);
        }

        // POST: api/manager/add-car
        [HttpPost("add-car")]
        public async Task<IActionResult> AddCar([FromForm] AddCarDTO addCarDto)
        {
            if (addCarDto == null)
            {
                return BadRequest("Car data is null.");
            }

            var carObj = new CarDTO()
            {
                Id = addCarDto.Id,
                Brand = addCarDto.Brand,
                BodyType = addCarDto.BodyType,
                Model = addCarDto.Model,
                Transmission = addCarDto.Transmission,
                FuelType = addCarDto.FuelType,
                NumberOfSeats = addCarDto.NumberOfSeats,
                PricePerHour = addCarDto.PricePerHour,
                AvailableFrom = addCarDto.AvailableFrom,
                AvailableTo = addCarDto.AvailableTo
            };
            if (addCarDto.ImagePath != null && addCarDto.ImagePath.Length > 0)
            {

                if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
                {
                    throw new ArgumentNullException(nameof(_webHostEnvironment.WebRootPath), "WebRootPath is not set. Make sure the environment is configured properly.");
                }

                var profileimagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "CarImages");


                if (!Directory.Exists(profileimagesPath))
                {
                    Directory.CreateDirectory(profileimagesPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(addCarDto.ImagePath.FileName);
                var imagePath = Path.Combine(profileimagesPath, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await addCarDto.ImagePath.CopyToAsync(stream);
                }


                carObj.ImagePath = "/CarImages/" + fileName;
            }
            else
            {
                carObj.ImagePath = null;
            }


            var addedCar = _managerRepository.AddCar(carObj);
            return Ok(addedCar);
        }

        // PUT: api/manager/update-car
        [HttpPut("update-car")]
        public IActionResult UpdateCar([FromForm] CarDTO carDto)
        {
            if (carDto == null)
            {
                return BadRequest("Car data is null.");
            }

            try
            {
                _managerRepository.UpdateCar(carDto);
                return Ok("Car updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating car: {ex.Message}");
            }
        }

        // DELETE: api/manager/delete-car/{carId}
        [HttpDelete("delete-car/{carId}")]
        public IActionResult DeleteCar(string carId)
        {
            try
            {
                _managerRepository.DeleteCar(carId);
                return Ok("Car deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting car: {ex.Message}");
            }
        }
    }
}
