using Car_rental.IRepository;
using Car_rental.Models.ResposeModel;
using Microsoft.AspNetCore.Mvc;

namespace Car_rental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
      
     
       private readonly INotificationRepository _notificationRepository;

            // Constructor to inject the repository dependency
            public NotificationController(INotificationRepository notificationRepository)
            {
                _notificationRepository = notificationRepository;
            }

            // The hero's first task: Getting all notifications
            [HttpGet]
            public ActionResult<ICollection<NotificationResponseDTO>> GetAllNotifications()
            {
                try
                {
                    var notifications = _notificationRepository.GetAllNotifications();
                    return Ok(notifications);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // The second task: Adding a new notification
            [HttpPost]
            public ActionResult AddNotification([FromBody] NotificationResponseDTO notification)
            {
                if (notification == null)
                {
                    return BadRequest("Notification is null.");
                }

                try
                {
                    _notificationRepository.AddNotification(notification);
                    return Ok("Notification added successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // The third task: Deleting (or marking as deleted) a notification
            [HttpDelete("{id}")]
            public ActionResult DeleteNotification(string id)
            {
                try
                {
                    _notificationRepository.DeleteNotification(id);
                    return Ok("Notification deleted successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            // The final task: Marking a notification as read
            [HttpPut("mark-as-read/{id}")]
            public ActionResult MarkAsRead(string id)
            {
                try
                {
                    _notificationRepository.MarkAsRead(id);
                    return Ok("Notification marked as read.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }
    }


