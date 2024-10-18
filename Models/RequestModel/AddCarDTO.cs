namespace Car_rental.Models.RequestModel
{
    public class AddCarDTO
    {
        public string CarId { get; set; }
        public string Brand { get; set; }
        public string BodyType { get; set; }
        public string Model { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public IFormFile? ImagePath { get; set; }
    }
}
