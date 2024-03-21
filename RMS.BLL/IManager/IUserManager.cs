using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;
using RMS.Utility;
using System.Collections;

namespace RMS.BLL.IManager
{
    public interface IUserManager
    {
        User GetUserById(long userId);
        ResponseModel SaveUser(User model);
        List<User> GetUsers();
        //List<User> GetBankUsersList(User moUser);
        bool ExistsUserManager(string userName, string password);
        //int UserActive(List<User> user);

        string[] UserRolesByUserName(string username);
        ResponseModel ResetUserPassword(User model);
        ResponseModel ChangePassword(string OldPassword, User model);
        ResponseModel ForgotPassword(string OldPassword, User model);

        bool IsUserInRole(string username, string roleName);
        int UpdateFailedAttempt(string userName);
        string GetErrorMessage(string userName);
        List<User> FindUser(string userName);
        User FindOne(string userName);

        List<UserBankInfo> GetUserBankInfos();
        UserBankInfo FindOneUserBankInfo(string userName);
        ResponseModel UpdateStatus(string UserId, bool status);

        int UpdateLogInStatus(string clientMachineName, string clientIP, long userId, bool IsValidate);

        string CheckLogin(long userID);
    }


 
}
