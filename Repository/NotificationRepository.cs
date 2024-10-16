using Car_rental.Entities;
using Car_rental.IRepository;
using Car_rental.Models.ResposeModel;
using Microsoft.Data.Sqlite;

namespace Car_rental.Repository
{

    public class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;

        public NotificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all notifications
        public ICollection<NotificationResponseDTO> GetAllNotifications()
        {
            try
            {
                var notificationList = new List<NotificationResponseDTO>();
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM Notifications";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var notification = new NotificationResponseDTO
                            {
                                NotificationId = reader.GetString(0),
                                RentalId = reader.GetString(1),
                                ReturnId = reader.GetString(2),
                                Status = reader.GetString(3),
                                // Optionally include other properties here, like creation date, etc.
                            };
                            notificationList.Add(notification);
                        }
                    }
                }
                return notificationList;
            }
            catch (Exception error)
            {
                throw new Exception($"Error retrieving notifications: {error.Message}");
            }
        }

        // Add a new notification
        public void AddNotification(Notification notification)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO Notifications(NotificationId, RentalId, ReturnId, Status) 
                        VALUES (@notificationId, @rentalId, @returnId, @status)";
                    command.Parameters.AddWithValue("@notificationId", notification.NotificationId);
                    command.Parameters.AddWithValue("@rentalId", notification.RentalId);
                    command.Parameters.AddWithValue("@returnId", notification.ReturnId);
                    command.Parameters.AddWithValue("@status", notification.Status);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Error adding notification: {error.Message}");
            }
        }

        // Mark a notification as deleted
        public void DeleteNotification(string notificationId)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Notifications SET IsDeleted = @isDeleted WHERE NotificationId = @notificationId";
                    command.Parameters.AddWithValue("@isDeleted", true);
                    command.Parameters.AddWithValue("@notificationId", notificationId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Error deleting notification: {error.Message}");
            }
        }

        // Mark a notification as read
        public void MarkAsRead(string notificationId)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Notifications SET Status = @status WHERE NotificationId = @notificationId";
                    command.Parameters.AddWithValue("@status", "Read");
                    command.Parameters.AddWithValue("@notificationId", notificationId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Error marking notification as read: {error.Message}");
            }
        }

        public void AddNotification(NotificationResponseDTO notification)
        {
            throw new NotImplementedException();
        }
    }
}


