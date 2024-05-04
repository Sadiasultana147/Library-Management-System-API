using LMS_BAL.AuthorsRepo;
using LMS_DATA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository iAuthorRepository;

        public AuthorController(IAuthorRepository _AuthorRepository)
        {
            iAuthorRepository = _AuthorRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> InsertPatientData(Author author)
        {
            try
            {
                await iAuthorRepository.InsertAuthorData(author);
                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthor()
        {
            var author = await iAuthorRepository.GetAllAuthor();
            return Ok(author);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAuthor(int id)
        {
            try
            {
                var result = await iAuthorRepository.DeleteAuthor(id);

                if (!result)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "Author not found" };

                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }

        [HttpGet("{authorID}")]
        public async Task<IActionResult> GetPatientById(int authorID)
        {
            try
            {
                var author = await iAuthorRepository.GetAuthorById(authorID);
                if (author == null)
                    return NotFound($"Patient ID not found");

                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateAuthor(int id, Author author)
        {
            try
            {
                if (id != author.AuthorID)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "IDs do not match" };

                var result = await iAuthorRepository.UpdateAuthor(author);

                if (!result)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "Author not found" };

                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }

    }
}
