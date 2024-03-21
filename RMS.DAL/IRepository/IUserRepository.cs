using RMS.Model;

namespace RMS.DAL.IRepository
{
    public interface IUserRepository:IRepository<User>
    {
        int UpdateUserLoginStatus(string clientMachineName, string clientIP, long userId, bool IsValidate);

        string CheckLogin(long userID);
    }
}
