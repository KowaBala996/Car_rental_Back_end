namespace Car_rental.Models.ResposeModel
{
   
        public class NotificationResponseDTO
        {
        public string NotificationId { get; set; }
        public string RentalId { get; set; }
        public string ReturnId { get; set; }
        public string Status { get; set; }

        public NotificationResponseDTO()
        {
            Status = "Unread"; // Default status can be set to Unread
        }
    }
    

}
