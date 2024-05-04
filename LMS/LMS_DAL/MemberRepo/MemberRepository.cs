using LMS_BAL.MembersRepo;
using LMS_DATA;
using LMS_DATA.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_DAL.MemberRepo
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task InsertMemberData(Member Member)
        {
            _context.Members.Add(Member);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Member>> GetAllMember()
        {
            return await _context.Members.ToListAsync();
        }
        public async Task<Member> GetMemberById(int memberID)
        {
            var member = await _context.Members.Where(x => x.MemberID == memberID).FirstOrDefaultAsync();
            if (member == null)
                Console.WriteLine($"Member ID not found.");
            // Map database fields to the view model properties
            var memberModel = new Member
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                RegistrationDate = member.RegistrationDate

            };
            return memberModel;
        }

        public async Task<bool> UpdateMember(Member member)
        {
            var result = await _context.Members.FindAsync(member.MemberID);

            if (result == null)
            {
                return false;
            }

            _context.Entry(result).CurrentValues.SetValues(member);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMemebr(int MemberId)
        {
            var member = await _context.Members.FindAsync(MemberId);
            if (member == null)
            {
                return false;
            }
            else
            {
                _context.Members.Remove(member);
                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
