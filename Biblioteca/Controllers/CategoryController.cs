using BibliotecaBLL.DTOs;
using BibliotecaBLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var result = await _categoryService.AddAsync(categoryDTO);
                return Ok(new ApiResponse<CategoryDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CategoryDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var result = await _categoryService.GetAsync(id);
                return Ok(new ApiResponse<CategoryDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CategoryDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async  Task<IActionResult> GetAllCategories() 
        {
            try
            {
                var result = await _categoryService.GetAllAsync();
                return Ok(new ApiResponse<List<CategoryDTO>>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CategoryDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                var result = await _categoryService.UpdateAsync(categoryDTO);
                return Ok(new ApiResponse<CategoryDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CategoryDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.Delete(id);
                return Ok(new ApiResponse<bool>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<CategoryDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
