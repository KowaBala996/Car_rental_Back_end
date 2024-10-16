using Car_rental.Entities;

namespace Car_rental.Models.ResposeModel
{
    public class CustomerDTO
    {
        public string Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Nic { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public string FrontImagePath { get; set; }
        public string ProofIdNumber { get; set; }
        public string profileStatus { get; set; }
    }
}
