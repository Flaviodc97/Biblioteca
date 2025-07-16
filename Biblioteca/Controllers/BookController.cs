using BibliotecaBLL.DTOs;
using BibliotecaBLL.Exceptions;
using BibliotecaBLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService) 
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            try
            {
                var result = await _bookService.AddAsync(bookDTO);
                return Ok(new ApiResponse<BookDTO>
                { 
                    Success = true,
                    Data = result
                });
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id) 
        {
            try
            {
                var result = await _bookService.GetAsync(id);
                return Ok(new ApiResponse<BookDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            try 
            {
                var response = await _bookService.GetAllAsync();
                return Ok(new ApiResponse<List<BookDTO>>
                {
                    Success = true,
                    Data = response
                });
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }

        }
        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookDTO bookDTO)
        {
            try
            {
                var result = await _bookService.UpdateAsync(bookDTO);
                return Ok(new ApiResponse<BookDTO> 
                {
                    Success = true,
                    Data = result
                });
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try 
            {
                var result = await _bookService.Delete(id);
                return Ok(new ApiResponse<BookDTO>
                {
                    Success = result
                });
            }
            catch (BookNotFoundException ex)
            {
                return NotFound(new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<BookDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }

        }

    }
}
