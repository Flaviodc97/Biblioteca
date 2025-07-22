using BibliotecaBLL.DTOs;
using BibliotecaBLL.DTOs.AuthorDTOS;
using BibliotecaBLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorDTO authorDTO)
        {
            try
            {
                var result = await _authorService.AddAsync(authorDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthorDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int  id)
        {
            try
            {
                var result = await _authorService.GetAsync(id);
                return Ok(new ApiResponse<AuthorDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthorDTO>
                { 
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var result = await _authorService.GetAllAsync();
                return Ok(new ApiResponse<List<AuthorDTO>>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthorDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("Search")]
        public  IActionResult SearchAuthor([FromQuery] AuthorSeachDTO authorSearchDTO)
        {
            try
            {
                var result = _authorService.SearchAuthorList(authorSearchDTO);
                return Ok(new ApiResponse<List<AuthorDTO>> 
                {
                    Success = true,
                    Data = result

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthorDTO>
                {
                    Success = false,
                    Message = ex.Message
                });            
            
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var result = await _authorService.Delete(id);
                return Ok( new ApiResponse<bool> 
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthorDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("AddBookToAuthor")]
        public async Task<IActionResult> AddBookToAthor(BookAuthorDTO bookAuthorDTO)
        {
            try
            {
                var result = await _authorService.AddBookToAuthorAsync(bookAuthorDTO);
                return Ok(new ApiResponse<AuthorWithBooksDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<BookAuthorDTO>
                {
                    Success = false,
                    Message = ex.Message

                });
            }
        }

        [HttpGet("GetAuthorsBooks")]
        public async Task<IActionResult> GetAuthorsBooks(int id)
        {
            try 
            {
                var result = await _authorService.GetAuthorWithBooks(id);
                return Ok(new ApiResponse<AuthorWithBooksDTO> 
                {
                    Success = true,
                    Data = result
                });
            }
            catch(Exception ex) 
            {
                return StatusCode(500, new ApiResponse<AuthorDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPatch("RemoveBooksFromAuthor")]
        public async Task<IActionResult> RemoveBooksFromAuthor(BookAuthorDTO bookAuthorDTO)
        {
            try 
            {
                var result = await _authorService.RemoveBookToAuthorAsync(bookAuthorDTO);
                return Ok(new ApiResponse<AuthorWithBooksDTO>
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<BookAuthorDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("GetAuthorsPaginated")]
        public async Task<IActionResult> GetAuthorsPaginated(int pageIndex, int pageSize)
        {
            try
            {
                var result = await _authorService.GetPaginatedListAsync(pageIndex, pageSize);
                return Ok(new ApiResponse<PaginatedListDTO<AuthorDTO>> 
                {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthorDTO>
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("GenericSearch")]
        public  IActionResult GenericSearch([FromBody] Dictionary<string, object> Params)
        {
            try
            {
                var result = _authorService.GenericSearch(Params);
                return Ok(new ApiResponse<List<AuthorDTO>> {
                    Success = true,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<AuthorDTO>
                {
                    Success = true,
                    Message = ex.Message
                });
            }
        }

    }
}
