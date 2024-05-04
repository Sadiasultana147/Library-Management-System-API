using LMS_BAL.BooksRepo;
using LMS_BAL.BorrowdBooksRepo;
using LMS_DATA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowdBookController : Controller
    {
        
        private readonly IBorrowdBooksRepository iBorrowdBooksRepository;

        public BorrowdBookController(IBorrowdBooksRepository _BorrowdBooksRepository)
        {
            iBorrowdBooksRepository = _BorrowdBooksRepository;
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> InsertBorrowedBookData(BorrowdBooks book)
        {
            try
            {
                await iBorrowdBooksRepository.InsertBorrowedBookData(book);
                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllBorrowdBook()
        {
            var books = await iBorrowdBooksRepository.GetAllBorrowedBook();
            return Ok(books);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteBorrowdBook(int id)
        {
            try
            {
                var result = await iBorrowdBooksRepository.DeleteBorrowdBooks(id);

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
        public async Task<IActionResult> GetBorrowdBookById(int ID)
        {
            try
            {
                var book = await iBorrowdBooksRepository.GetBorrowBookById(ID);
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
        public async Task<ActionResult<ApiResponse<bool>>> UpdateBorrowedBook(int id, BorrowdBooks book)
        {
            try
            {
                if (id != book.BookID)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "IDs do not match" };

                var result = await iBorrowdBooksRepository.UpdateBorrowedBook(book);

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
