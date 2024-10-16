using Car_rental.Entities;
using Car_rental.Models.ResposeModel;

namespace Car_rental.IRepository
{
    public interface INotificationRepository
    {
        ICollection<NotificationResponseDTO> GetAllNotifications();

        void AddNotification(Notification notification);

        void DeleteNotification(string notificationId);

        void MarkAsRead(string notificationId);
        void AddNotification(NotificationResponseDTO notification);
    }
}
