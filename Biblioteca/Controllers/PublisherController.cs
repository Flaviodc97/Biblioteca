using BibliotecaBLL.DTOs;
using BibliotecaBLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publishService;
        public PublisherController(IPublisherService publisherService)
        {
            _publishService = publisherService;
        }
        [HttpPost]
        public async Task<IActionResult> AddPublisher(PublisherDTO publisherDTO) 
        {
            try
            {
                var result = await _publishService.AddAsync(publisherDTO);
                return Ok(new ApiResponse<PublisherDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<PublisherDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            try
            {
                var result = await _publishService.GetAsync(id);
                return Ok(new ApiResponse<PublisherDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<PublisherDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _publishService.GetAllAsync();
                return Ok(new ApiResponse<List<PublisherDTO>>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<PublisherDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePublisher(PublisherDTO publisherDTO)
        {
            try
            {
                var result = await _publishService.UpdateAsync(publisherDTO);
                return Ok(new ApiResponse<PublisherDTO>
                {
                    Success = true,
                    Data = publisherDTO
                });
            }
            catch (Exception ex)
            { 
                return StatusCode(500, new ApiResponse<PublisherDTO>
                    {
                        Success = false,
                        Message = ex.Message
                    });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePublisher(int id)
        {
            try
            {
                var result = await _publishService.Delete(id);
                return Ok(new ApiResponse<bool> 
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<PublisherDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
        

    }
}
