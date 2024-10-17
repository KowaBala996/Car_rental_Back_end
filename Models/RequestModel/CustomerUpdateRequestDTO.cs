namespace Car_rental.Models.RequestModel
{
    public class CustomerUpdateRequestDTO
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public IFormFile? FrontImagePath { get; set; }
        public string ProofIdNumber { get; set; }
        public string ProfileStatus { get; set; }
    }
}
