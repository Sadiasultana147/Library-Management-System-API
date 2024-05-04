using LMS_BAL.AuthorsRepo;
using LMS_BAL.MembersRepo;
using LMS_BAL.UserRepo;
using LMS_DATA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : Controller
    {
        private readonly IMemberRepository iMemberRepository;
        private readonly IUserRepository iUserRepository;

        public MemberController(IMemberRepository _MemberRepository, IUserRepository _UserRepository)
        {
            iMemberRepository = _MemberRepository;
            iUserRepository= _UserRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<bool>>> InsertMemberData(Member member)
        {
            try
            {
                await iMemberRepository.InsertMemberData(member);
                UserInfo user = new UserInfo();
                user.UserId= member.UserId;
                await iUserRepository.UpdateUserRole(user);
                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAllMember()
        {
            var members = await iMemberRepository.GetAllMember();
            return Ok(members);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteMemebr(int id)
        {
            try
            {
                var result = await iMemberRepository.DeleteMemebr(id);

                if (!result)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "member not found" };

                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetMemberById(int ID)
        {
            try
            {
                var member = await iMemberRepository.GetMemberById(ID);
                if (member == null)
                    return NotFound($"Memebr ID not found");

                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> UpdateMemebr(int id, Member member)
        {
            try
            {
                if (id != member.MemberID)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "IDs do not match" };

                var result = await iMemberRepository.UpdateMember(member);

                if (!result)
                    return new ApiResponse<bool> { Success = false, ErrorMessage = "Member not found" };

                return new ApiResponse<bool> { Success = true, Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
