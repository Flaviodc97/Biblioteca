using BibliotecaBLL.DTOs;
using BibliotecaBLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDTO userDTO)
        {
            try
            {
                var response = await _userService.AddAsync(userDTO);
                return Ok(new ApiResponse<UserDTO>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<UserDTO>
                {
                    Success = false,
                    Message = ex.Message
                });         
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var response = await _userService.GetAsync(id);
                return Ok(new ApiResponse<UserDTO>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<UserDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAlllUser( )
        {
            try
            {
                var response = await _userService.GetAllAsync();
                return Ok(new ApiResponse<List<UserDTO>>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<UserDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDTO userDTO)
        {
            try
            {
                var response = await _userService.UpdateAsync(userDTO);
                return Ok(new ApiResponse<UserDTO>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<UserDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var response = await _userService.Delete(id);
                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<UserDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
