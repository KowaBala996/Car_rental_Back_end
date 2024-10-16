namespace Car_rental.Entities
{
    public class Customer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string nic { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string postalCode { get; set; }
        public string drivingLicenseNumber { get; set; }
        public string? FrontImagePath { get; set; }
        public string proofIdNumber { get; set; }
        public string profileStatus { get; set; }

    }
}
