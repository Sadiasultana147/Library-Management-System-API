using LMS_BAL.UserRepo;
using LMS_DATA;
using LMS_DATA.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_DAL.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUser(UserInfo user)
        {
            _context.UserInfo.Add(user); 
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<UserInfo> LoginUser(string email, string password)
        {
            var user = await _context.UserInfo.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user;
        }
        public async Task<bool> UpdateUserRole(UserInfo user)
        {
            var result = await _context.UserInfo.FindAsync(user.UserId);

            if (result == null)
            {
                return false;
            }

            result.IsMember = true;
            result.Role = "Member";

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserInfo>> GetAllUser()
        {
            return await _context.UserInfo.ToListAsync();
        }
    }
}
