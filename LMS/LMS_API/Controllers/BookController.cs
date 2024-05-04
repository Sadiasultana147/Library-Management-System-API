using LMS_BAL.BooksRepo;
using LMS_BAL.MembersRepo;
using LMS_DAL.MemberRepo;
using LMS_DATA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LMS_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository iBookRepository;

        public BookController(IBookRepository _BookRepository)
        {
            iBookRepository = _BookRepository;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> InsertBookData(Book book)
        {
            try
            {
                await iBookRepository.InsertBookData(book);
                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllBook()
        {
            var books = await iBookRepository.GetAllBook();
            return Ok(books);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteBook(int id)
        {
            try
            {
                var result = await iBookRepository.DeleteBook(id);

                if (!result)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "book not found" };

                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetBookById(int ID)
        {
            try
            {
                var book = await iBookRepository.GetBookById(ID);
                if (book == null)
                    return NotFound($"Book ID not found");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateBook(int id, Book book)
        {
            try
            {
                if (id != book.BookID)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "IDs do not match" };

                var result = await iBookRepository.UpdateBook(book);

                if (!result)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "Book not found" };

                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
