
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HasCode;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class UserManager : BaseManager, IUserManager
    {
        private IUserRepository _userRepository;
        private IUserBankInfoRepository _userBankInfoRepository;

        public UserManager()
        {
            _userRepository = new UserRepository();
            _userBankInfoRepository = new UserBankInfoRepository();

        }

        public User GetUserById(long userId)
        {
            return _userRepository.FindOne(x => x.UserId == userId);
        }

        public ResponseModel SaveUser(User model)
        {
           
                var sc = new IBSHasCode();
                string pass = "";
                pass = sc.CreateDoubleHas(model.UserName, model.Password);
                User oldData = _userRepository.FindOne(x => x.UserName == model.UserName);
                if (oldData == null)
                {
                    DateTime dt = DateTime.Now;
                    DateTime AM = dt.AddDays(Convert.ToInt16(model.PasswordValidity));
                    var aUser = new User
                    {
                        UserId = model.UserId,
                        UserName = model.UserName,
                        IsAnonymous = false,
                        Password = pass,
                        ConfirmPassword = pass,
                        SecurityQustion = model.SecurityQustion,
                        Answer = model.Answer,
                        RoleId = model.RoleId,
                        LastActivityDate = DateTime.Now,
                        TransLimit = 0,
                        VerifyLimit =0,
                        ApprovedUser = true,
                        BankCode = model.BankCode,
                        BranchInfoIdentity = model.BranchInfoIdentity,
                        Createdby = PortalContext.CurrentUser.UserName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        LoweredUserName = model.UserName.ToLower(),
                        UserFullName = model.UserFullName,
                        ParmitedBy = PortalContext.CurrentUser.UserName,
                        UserExpiryDate = AM,
                        PasswordValidity = 90,
                        LasUpdateDate = DateTime.Now,
                        AttemptCount = 0,
                        WinPassword = model.WinPassword,
                        IsFirstTime = true,
                        IsLogin = 0,
                        IPAddress = "",
                        HostName = "",
                    };

                    ResponseModel.ResponsStatus = _userRepository.Save(aUser);
                    ResponseModel.Message = "User has been saved successfully. user ID: " + aUser.UserName;
                }
                else
                {
                    oldData.RoleId = model.RoleId;
                    oldData.PhoneNumber = model.PhoneNumber;
                    oldData.TransLimit = model.TransLimit ?? 0;
                    oldData.VerifyLimit = model.VerifyLimit ?? 0;
                    oldData.AttemptCount = 0;
                    oldData.BankCode = model.BankCode;
                    oldData.BranchInfoIdentity = model.BranchInfoIdentity;
                    oldData.UserFullName = model.UserFullName;
                    oldData.VerifyLimit = model.VerifyLimit;
                    oldData.LasUpdateDate = DateTime.Now;
                    oldData.WinPassword = model.WinPassword;
                    ResponseModel.ResponsStatus = _userRepository.Edit(oldData);
                    ResponseModel.Message = "User has been pdated successfully.";

                }
            return ResponseModel;

        }

        public List<User> GetUsers()
        {
            return
                _userRepository.Filter(x => x.BankCode == PortalContext.CurrentUser.BankCode)
                    //.Include(x => x.BranchInfo)
                    .Include(x => x.Role)
                    .ToList();
        }

        public bool ExistsUserManager(string userName, string password)
        {

            var sc = new IBSHasCode();
            string pass = "";
            pass = sc.CreateDoubleHas(userName, password);
            return _userRepository.Exists(x => x.UserName == userName && x.Password == pass && x.AttemptCount <= 5);
        }

        //User Role Authentication
        public string[] UserRolesByUserName(string username)
        {
            return _userRepository.Filter(x => x.UserName == username)
                  .Include(x => x.Role)
                  .Select(x => x.Role.RoleName)
                  .ToArray();
        }

        public ResponseModel ResetUserPassword(User model)
        {
            var sc = new IBSHasCode();
            string pass = "";
            pass = sc.CreateDoubleHas(model.UserName, "ITIL@123");
            var rec = _userRepository.FindOne(x => x.UserName == model.UserName);
            if (rec != null)
            {

                if (rec.UserName == PortalContext.CurrentUser.UserName)
                {
                    ResponseModel.ResponsStatus = 0;
                    ResponseModel.Message = "Permission not allow to reset password because of same User, must be another user to reset the password.";
                }
                else
                {
                    if (rec.RoleId == 1)
                    {
                        if (PortalContext.CurrentUser.LoweredRoleName == "S")
                        {
                            if (!rec.WinPassword)
                            {
                                rec.IsFirstTime = true;
                            }
                            rec.Password = pass;
                            rec.ConfirmPassword = pass;
                            rec.PasswordChangeDate = DateTime.Now;
                            rec.AttemptCount = 0;
                            ResponseModel.ResponsStatus = _userRepository.Edit(rec);
                            ResponseModel.Message = "Password has been reset, New Password: ITIL@123, Note: Please change this password after login.";
                        }
                        else
                        {
                            ResponseModel.ResponsStatus = 0;
                            ResponseModel.Message = "Must be admin user to reset the password.";
                        }
                    }
                    else
                    {
                        if (!rec.WinPassword)
                        {
                            rec.IsFirstTime = true;
                        }
                        rec.Password = pass;
                        rec.ConfirmPassword = pass;
                        rec.PasswordChangeDate = DateTime.Now;
                        rec.AttemptCount = 0;
                        ResponseModel.ResponsStatus = _userRepository.Edit(rec);
                        ResponseModel.Message = "Password has been reset, New Password: ITIL@123, Note: Please change this password after login.";

                    }

                }
            }
            else
            {
                ResponseModel.Message = "User ID Not Match";
            }
            return ResponseModel;
        }

        public ResponseModel ChangePassword(string OldPassword, User model)
        {

            IBSHasCode sc = new IBSHasCode();

            string UserName = "";
            UserName = PortalContext.CurrentUser.UserName;
            string enOldPassword = "";
            enOldPassword = sc.CreateDoubleHas(UserName, OldPassword);
            string Pass = "";
            Pass = sc.CreateDoubleHas(UserName, model.Password);
            var rec = _userRepository.FindOne(x => x.UserName == UserName);
            if (rec.Password == enOldPassword)
            {
                if (!rec.WinPassword)
                {
                    rec.IsFirstTime = false;
                }
                rec.Password = Pass;
                rec.ConfirmPassword = Pass;
                rec.PasswordChangeDate = DateTime.Now;
                ResponseModel.ResponsStatus = _userRepository.Edit(rec);
                ResponseModel.Message = "Password has been Changed successfully.";
            }
            else
            {
                ResponseModel.Message = "Old Password Not Match";
            }
            return ResponseModel;
        }
        public ResponseModel ForgotPassword(string OldPassword, User model)
        {

            IBSHasCode sc = new IBSHasCode();

            string UserName = "";
            UserName = model.UserName;
            string enOldPassword = "";
            //enOldPassword = sc.CreateDoubleHas(UserName, OldPassword);
            string Pass = "";
            Pass = sc.CreateDoubleHas(UserName, model.Password);
            var rec = _userRepository.FindOne(x => x.UserName == UserName);
            if (rec.Password == OldPassword)
            {
                if (!rec.WinPassword)
                {
                    rec.IsFirstTime = false;
                }
                rec.Password = Pass;
                rec.ConfirmPassword = Pass;
                rec.PasswordChangeDate = DateTime.Now;
                ResponseModel.ResponsStatus = _userRepository.Edit(rec);
                ResponseModel.Message = "Password has been Changed successfully.";
            }
            else
            {
                ResponseModel.Message = "Old Password Not Match";
            }
            return ResponseModel;
        }
        public int UpdateFailedAttempt(string userName)
        {
            var userInfo = _userRepository.FindOne(x => x.UserName == userName);
            int isSusscced = 0;
            if (userInfo != null)
            {
                userInfo.AttemptCount = userInfo.AttemptCount + 1;
                _userRepository.Edit(userInfo);
                isSusscced = 1;
            }
            return isSusscced;
        }

        public string GetErrorMessage(string userName)
        {
            string msg = "";
            var userInfo = _userRepository.FindOne(x => x.UserName == userName);
            if (userInfo != null)
            {
                if (userInfo.AttemptCount > 5)
                {
                    msg = "You are trying to login with the wrong password more than five times. Please contact Head Office Admin.";
                }
                else if (userInfo.ApprovedUser == false)
                {
                    msg = "Your login ID is inactive.";
                }
                else
                {
                    msg = "Invalid username or password.";
                }
            }
            return msg;
        }

        public List<User> FindUser(string userName)
        {
            return _userRepository.Filter(x => x.UserName.ToLower() == userName.ToLower()).ToList();
        }

        public User FindOne(string userName)
        {
            return _userRepository.Filter(x => x.UserName == userName).Include(x=>x.Role).FirstOrDefault();
        }

        public List<UserBankInfo> GetUserBankInfos()
        {
            List<UserBankInfo> userBankInfos;
            if (PortalContext.CurrentUser.LoweredRoleName == "S")
            {
                userBankInfos = _userBankInfoRepository.All().ToList();
            }
            else
            {
                userBankInfos = _userBankInfoRepository.Filter(x => x.RoleName != "AdminUser").ToList();
            }

            return userBankInfos;
        }

        public UserBankInfo FindOneUserBankInfo(string userName)
        {
            UserBankInfo userBankInfo = _userBankInfoRepository.FindOne(x => x.UserName == userName);
            return userBankInfo;
        }

        public ResponseModel UpdateStatus(string UserId, bool status)
        {
            long userId = Convert.ToInt32(UserId);
            User oldData = _userRepository.FindOne(x => x.UserId == userId);
            if (oldData != null)
            {
                oldData.ApprovedUser = status;
                ResponseModel.ResponsStatus = _userRepository.Edit(oldData);
                ResponseModel.Message = status ? "User has been activated." : "User has been in active.";
            }
            else
            {
                ResponseModel.Message = "User not found.";
            }
            return ResponseModel;
        }

        public int UpdateLogInStatus(string clientMachineName, string clientIP, long userId, bool IsValidate)
        {
            return _userRepository.UpdateUserLoginStatus(clientMachineName, clientIP, userId, IsValidate);
        }

        public string CheckLogin(long userID)
        {
            return _userRepository.CheckLogin(userID);
        }

        public bool IsUserInRole(string username, string roleName)
        {

            return _userRepository.Filter(x => x.UserName == username)
                .Include(x => x.Role)
                .Any(x => x.Role.RoleName == roleName);
        }
    }
}
