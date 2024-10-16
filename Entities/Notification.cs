namespace Car_rental.Entities
{
    public class Notification
    {
        public string NotificationId { get; set; }
        public string RentalId { get; set; }
        public string ReturnId { get; set; }
        public string Status { get; set; } // E.g., Unread, Read

        public Notification()
        {
            Status = "Unread"; // Default status can be set to Unread
        }
    }
}
