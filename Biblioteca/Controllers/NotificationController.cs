using BibliotecaBLL.DTOs;
using BibliotecaBLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNotification(NotificationDTO notificationDTO)
        {
            try
            {
                var response = await _notificationService.AddAsync(notificationDTO);
                return Ok(new ApiResponse<NotificationDTO>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NotificationDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id)
        {
            try
            {
                var response = await _notificationService.GetAsync(id);
                return Ok(new ApiResponse<NotificationDTO>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NotificationDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotification()
        {
            try
            {
                var response = await _notificationService.GetAllAsync();
                return Ok(new ApiResponse<List<NotificationDTO>>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NotificationDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNotification(NotificationDTO notificationDTO)
        {
            try
            {
                var response = await _notificationService.UpdateAsync(notificationDTO);
                return Ok(new ApiResponse<NotificationDTO>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NotificationDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {
                var response = await _notificationService.Delete(id);
                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<NotificationDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
        
    }
}
