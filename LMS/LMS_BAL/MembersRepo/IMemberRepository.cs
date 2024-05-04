using LMS_DATA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_BAL.MembersRepo
{
    public interface IMemberRepository
    {
        Task InsertMemberData(Member Member);
        Task<bool> DeleteMemebr(int MemberId);
        Task<IEnumerable<Member>> GetAllMember();
        Task<Member> GetMemberById(int memberID);
        Task<bool> UpdateMember(Member member);
    }
}
