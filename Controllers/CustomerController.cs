using Car_rental.Entities;
using Car_rental.IRepository;
using Car_rental.Models.RequestModel;
using Car_rental.Models.ResposeModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CustomerController(ICustomerRepository customerRepository, IWebHostEnvironment webHostEnvironment)
        {
            _customerRepository = customerRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("Add-Customer")]
        public async Task<IActionResult> AddCustomer([FromForm] RequestCustomerDTO requestCustomerDto)
        {
            if (requestCustomerDto == null)
            {
                return BadRequest("Invalid customer data.");
            }

            var customer = new Customer()
            {
                id = requestCustomerDto.Id,
                name = requestCustomerDto.Name,
                phone = requestCustomerDto.Phone,
                email = requestCustomerDto.Email,
                nic = requestCustomerDto.Nic,
                password = requestCustomerDto.Password
                

            };



            _customerRepository.AddCustomer(customer);
            return Ok(customer);
        }

        [HttpGet("Get-Customer-By-Nic/{nic}")]
        public ActionResult<CustomerDTO> GetCustomerById(string nic)
        {
            var customer = _customerRepository.GetCustomerById(nic);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }
            return Ok(customer);
        }

        [HttpGet("Get-All-Customer")]
        public ActionResult<List<CustomerDTO>> GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            return Ok(customers);
        }


        [HttpPut("Update-Customer")]
        public async Task<IActionResult> UpdateCustomer([FromForm] CustomerUpdateRequestDTO requestCustomerDto)
        {
            if (requestCustomerDto == null)
            {
                return BadRequest("Invalid customer data.");
            }

            var CustomerUpdateDTO = new CustomerUpdateDTO()
            {
                Id = requestCustomerDto.Id,
                Address = requestCustomerDto.Address,
                PostalCode = requestCustomerDto.PostalCode,
                DrivingLicenseNumber = requestCustomerDto.DrivingLicenseNumber,
                ProofIdNumber = requestCustomerDto.ProofIdNumber,
                ProfileStatus = requestCustomerDto.ProfileStatus
            };
            if (requestCustomerDto.FrontImagePath != null && requestCustomerDto.FrontImagePath.Length > 0)
            {

                if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
                {
                    throw new ArgumentNullException(nameof(_webHostEnvironment.WebRootPath), "WebRootPath is not set. Make sure the environment is configured properly.");
                }

                var profileimagesPath = Path.Combine(_webHostEnvironment.WebRootPath, "LicenceFrontImages");


                if (!Directory.Exists(profileimagesPath))
                {
                    Directory.CreateDirectory(profileimagesPath);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(requestCustomerDto.FrontImagePath.FileName);
                var imagePath = Path.Combine(profileimagesPath, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await requestCustomerDto.FrontImagePath.CopyToAsync(stream);
                }


                CustomerUpdateDTO.FrontImagePath = "/LicenceFrontImages/" + fileName;
            }
            else
            {
                CustomerUpdateDTO.FrontImagePath = null;
            }

            _customerRepository.UpdateCustomer(CustomerUpdateDTO);
            return NoContent();

        }


        [HttpDelete("Delete-Customer/{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            _customerRepository.DeleteCarAndCustomers(id);
            return NoContent();
        }
    }
}
