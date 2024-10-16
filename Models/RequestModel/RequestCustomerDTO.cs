namespace Car_rental.Models.RequestModel
{
    public class RequestCustomerDTO
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Nic { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public IFormFile? FrontImagePath { get; set; }
        public string ProofIdNumber { get; set; }
        public string ProfileStatus { get; internal set; }
    }
}
