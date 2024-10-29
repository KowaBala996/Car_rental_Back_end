namespace Car_rental.Models.ResposeModel
{
    public class CarDTO
    {
        public string CarId { get; set; }
        public string Brand { get; set; }
        public string BodyType { get; set; }
        public string Model { get; set; }
        public string Transmission { get; set; }
        public string FuelType { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal PricePerHour { get; set; }
        public string? ImagePath { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string Id { get;  set; }
    }
}
