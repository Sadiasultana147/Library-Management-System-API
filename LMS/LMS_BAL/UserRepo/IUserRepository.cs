using LMS_DATA.ViewModels;


namespace LMS_BAL.UserRepo
{
    public interface IUserRepository
    {
        Task<bool> RegisterUser(UserInfo user);
        Task<UserInfo> LoginUser(string email, string password);
        Task<bool> UpdateUserRole(UserInfo user);
        Task<IEnumerable<UserInfo>> GetAllUser();
    }
}
