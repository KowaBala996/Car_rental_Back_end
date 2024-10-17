namespace Car_rental.Models.RequestModel
{
    public class CustomerUpdateDTO
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string? FrontImagePath { get; set; }
        public string ProofIdNumber { get; set; }
        public string ProfileStatus { get; set; }
    }
}
