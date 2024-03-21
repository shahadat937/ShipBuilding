using System;
using System.Web.Security;
using RMS.BLL.IManager;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Providers
{

    public class CustomMemberShipProvider : MembershipProvider
    {
        private IUserManager userManager;
        private IBankInfoManager bankInfoManager;
        public CustomMemberShipProvider()
        {
            userManager = new UserManager();
            bankInfoManager = new BankInfoManager();
        }
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var isExist = userManager.ExistsUserManager(username, password);
            if (isExist)
            {
                User user = userManager.FindOne(username);
                BankInfo bankInfo = bankInfoManager.FindOne(user.BranchInfoIdentity);
                var aPortalUser = new PortalUser
                {
                    BranchInfoIdentity = user.BranchInfoIdentity,
                    Code = bankInfo.Code,
                    BankCode = user.BankCode,
                    BankName = bankInfo.BankName,
                    DistrictCodeIdentity = Convert.ToInt64(bankInfo.DistrictCode),
                    DistrictName = bankInfo.DistrictName,
                    BranchCode = bankInfo.BranchCode,
                    BranchName = bankInfo.BranchName,
                    SubBranchCode = bankInfo.SubBranchCode,
                    SubBranchName = bankInfo.SubBranchName,
                    //CountryCode = Convert.ToInt32(user.CountryCode),
                    UserId = user.UserId,
                    UserName = user.UserName,
                    UserFullName = user.UserFullName,
                    IsValidate = user.ApprovedUser,
                    RoleId = user.RoleId,
                    UserRole = user.Role.RoleName,
                    LoweredRoleName = user.Role.LoweredRoleName,
                    WinPassword = user.WinPassword,
                    IsFirstTime = user.IsFirstTime,
                    //MenuTitle = (user.UserName + ", " + user.AspUserFullName).Replace(".", string.Empty)
                };
                PortalContext.CurrentUser = aPortalUser;

                UserBankInfo userBankInfo = userManager.FindOneUserBankInfo(user.UserName);
                PortalContext.CurrentUser.IsFirstTime = Convert.ToBoolean(userBankInfo.IsFirstTime);
            }
            return isExist;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}