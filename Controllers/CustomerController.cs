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

        [HttpPost]
            public async Task<IActionResult> AddCustomer([FromForm] RequestCustomerDTO requestCustomerDto)
            {
                if (requestCustomerDto == null)
                {
                    return BadRequest("Invalid customer data.");
                }

            var customer = new Customer()
            {
                name = requestCustomerDto.Name,
                phone = requestCustomerDto.Phone,
                email = requestCustomerDto.Email,
                nic = requestCustomerDto.Nic,
                password = requestCustomerDto.Password,
                address = requestCustomerDto.Address,
                postalCode = requestCustomerDto.PostalCode,
                drivingLicenseNumber = requestCustomerDto.DrivingLicenseNumber,
                proofIdNumber = requestCustomerDto.ProofIdNumber,
                profileStatus = requestCustomerDto.ProfileStatus
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


                customer.FrontImagePath = "/LicenceFrontImages/" + fileName;
            }
            else
            {
                customer.FrontImagePath = null;
            }

            _customerRepository.AddCustomer(customer);
                return Ok(customer);
            }

            [HttpGet("{nic}")]
            public ActionResult<CustomerDTO> GetCustomerById(int nic)
            {
                var customer = _customerRepository.GetCustomerById(nic);
                if (customer == null)
                {
                    return NotFound("Customer not found.");
                }
                return Ok(customer);
            }

            [HttpPut("{id}")]
            public IActionResult UpdateCustomer(int id, [FromForm] RequestCustomerDTO requestCustomerDto)
            {
                if (requestCustomerDto == null)
                {
                    return BadRequest("Invalid customer data.");
                }

                _customerRepository.UpdateCustomer(id, requestCustomerDto);
                return NoContent(); 

            }

            
            [HttpDelete("{id}")]
            public IActionResult DeleteCustomer(int id)
            {
                _customerRepository.DeleteCustomer(id);
                return NoContent(); 
            }
        }
}
