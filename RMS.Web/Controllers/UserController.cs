using System;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Security;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using System.Text.RegularExpressions;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {


        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly IBranchInfoManager _branchInfoManager;

        public UserController(IUserManager userManager, IRoleManager roleManager, ICommonCodeManager commonCodeManager, IBranchInfoManager branchInfoManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _commonCodeManager = commonCodeManager;
            _branchInfoManager = branchInfoManager;

        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult Index(UserViewModel model)
        {
            model.Users = _userManager.GetUsers();
            return View(model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult SearchByKey(UserViewModel model)
        {
            string searchKey = model.SearchKey;
            if (searchKey == null)
            {
                model.UserBankInfos = _userManager.GetUserBankInfos();
            }
            else
            {
                searchKey = searchKey.ToLower();
                model.UserBankInfos = _userManager.GetUserBankInfos().Where(x => x.BranchName.ToLower().Contains(searchKey) || x.UserName.ToLower().Contains(searchKey)).ToList();
                if (!model.UserBankInfos.Any())
                {
                    model.IsSucceed = 0;
                    model.Message = "Data is not found.";
                }
            }
            return View("Index", model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult ApproveUserIndex(UserViewModel model)
        {
            model.UserBankInfos = _userManager.GetUserBankInfos();
            return View(model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpGet]
        public ActionResult ActiveUser(string UserId, UserViewModel model)
        {
            bool status = true;
            ResponseModel response = _userManager.UpdateStatus(UserId, status);
            model.UserBankInfos = _userManager.GetUserBankInfos().ToList();
            return View("ApproveUserIndex", model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult SearchByKeyforStatus(UserViewModel model)
        {
            string searchKey = model.SearchKey;
            if (searchKey == null)
            {
                model.UserBankInfos = _userManager.GetUserBankInfos();
            }
            else
            {
                searchKey = searchKey.ToLower();
                model.UserBankInfos = _userManager.GetUserBankInfos().Where(x => x.BranchName.ToLower().Contains(searchKey) || x.UserName.ToLower().Contains(searchKey)).ToList();
            }
            return View("ApproveUserIndex", model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpGet]
        public ActionResult InactiveUser(string UserId, UserViewModel model)
        {
            bool status = false;
            ResponseModel response = _userManager.UpdateStatus(UserId, status);
            model.UserBankInfos = _userManager.GetUserBankInfos().ToList();
            return View("ApproveUserIndex", model);
        }
        //public ActionResult ActiveUser(UserViewModel model)
        //{
        //    model.Users = _userManager.GetBankUsersList(model);
        //    ModelState.Clear();
        //    return View(model);
        //}
        //public ActionResult UserActive(UserViewModel model)
        //{
        //    int count = _userManager.UserActive(model.Users);
        //    if (count > 0)
        //    {    
        //        TempData["Msg"] = "Data Successfully updated.";
        //        return RedirectToAction("ActiveUser", model);
        //    }
        //    else
        //    {
        //        TempData["Msg"] = "Data is not updated.";

        //    }
        //    return View(model);

        //}

        //[Authorize(Roles = "UserManagement,AdminUser")]
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CheckExistingUserName(UserViewModel model)
        {
            bool result;
            //User user = _userManager.FindOne(model.User.UserName);
            UserBankInfo userBankInfo = _userManager.FindOneUserBankInfo(model.User.UserName);
            if (userBankInfo != null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return Json(result);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpGet]
        public ActionResult Edit(string UserId, UserViewModel model)
        {
            ModelState.Clear();
            model.BankList = _branchInfoManager.GetBankList();
            //model.RoleList = _roleManager.GetAll();
            model.RoleList = PortalContext.CurrentUser.LoweredRoleName == "S" ? _roleManager.GetAll() : _roleManager.GetAll().Where(x => x.LoweredRoleName != "S").ToList();

            if (UserId != null)
            {
                long userId = Convert.ToInt32(UserId);
                if (userId > 0)
                {
                    model.User = _userManager.GetUserById(userId);
                    model.BankInfo = _branchInfoManager.GetBankInfo(model.User.BranchInfoIdentity);
                    model.BankCode = model.BankInfo.BankCode;
                    model.DistrictList = _branchInfoManager.GetDistrictList(model.BankCode);
                    model.DistrictCode = model.BankInfo.DistrictCode;
                    model.BranchList = _branchInfoManager.GetBranchList(model.DistrictCode);
                    model.BranchCode = model.BankInfo.Code;
                }
            }
            //BranchInfo oldData = _userManager.FindOneUserBankInfo.FindOneUserBankInfo(UserId);
            return View(model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult Save(UserViewModel model)
        {
            try
            {
                BranchInfo branchInfoForBrLevel = new BranchInfo();
                if (model.User.WinPassword)
                {
                    var branchInfo = _branchInfoManager.FineOneByBranchCode(model.DistrictCode);
                    if (branchInfo.BranchLevel != "3")
                    {
                        //model.Message = "Must be Head Office and Branch Office user for AD.";
                        model.IsSucceed = 0;
                        model.BankList = _branchInfoManager.GetBankList();
                        model.RoleList = PortalContext.CurrentUser.LoweredRoleName == "S" ? _roleManager.GetAll() : _roleManager.GetAll().Where(x => x.LoweredRoleName != "S").ToList();
                        return View("Edit", model);
                    }
                    if (model.AgentCode != null)
                    {
                        model.Message = "AD user not allow for Exchange House and Agent.";
                        model.IsSucceed = 0;
                        model.BankList = _branchInfoManager.GetBankList();
                        model.RoleList = PortalContext.CurrentUser.LoweredRoleName == "S" ? _roleManager.GetAll() : _roleManager.GetAll().Where(x => x.LoweredRoleName != "S").ToList();
                        return View("Edit", model);
                    }
                    model.User.Password = "123456";
                    model.User.ConfirmPassword = "123456";
                }
                if (model.AgentCode != null)
                {
                    branchInfoForBrLevel = _branchInfoManager.FineOneByBranchCode(model.AgentCode);
                    model.User.BranchInfoIdentity = branchInfoForBrLevel.BranchInfoIdentity;
                }
                else
                {
                    branchInfoForBrLevel = _branchInfoManager.FineOneByBranchCode(model.DistrictCode);
                    model.User.BranchInfoIdentity = branchInfoForBrLevel.BranchInfoIdentity;

                }
                if (model.User.RoleId == 1)
                {
                    if (PortalContext.CurrentUser.LoweredRoleName == "S")
                    {
                        if (branchInfoForBrLevel.BranchLevel == "4")
                        {
                          //ResponseModel response=  SaveFromAPI(model.User);
                          //model.Message = response.Message;
                          //model.IsSucceed = response.ResponsStatus;
                        }
                        else
                        {
                            ResponseModel = _userManager.SaveUser(model.User);
                            model.Message = ResponseModel.Message;
                            model.IsSucceed = ResponseModel.ResponsStatus;
                        }
                    }
                    else
                    {
                        model.Message = "Permission not allow to create admin user.";
                        model.IsSucceed = 0;
                        return View("Edit", model);
                    }

                }
                else
                {
                    if (branchInfoForBrLevel.BranchLevel == "4")
                    {
                        //ResponseModel response = SaveFromAPI(model.User);
                        //model.Message = response.Message;
                        //model.IsSucceed = response.ResponsStatus;
                    }
                    else
                    {
                        ResponseModel = _userManager.SaveUser(model.User);
                        model.Message = ResponseModel.Message;
                        model.IsSucceed = ResponseModel.ResponsStatus;
                    }
                }
                model.BankList = _branchInfoManager.GetBankList();
                model.RoleList = PortalContext.CurrentUser.LoweredRoleName == "S" ? _roleManager.GetAll() : _roleManager.GetAll().Where(x => x.LoweredRoleName != "S").ToList();

                return View("Edit", model);
            }
            catch (Exception ex)
            {
                model.BankList = _branchInfoManager.GetBankList();
                model.RoleList = PortalContext.CurrentUser.LoweredRoleName == "S" ? _roleManager.GetAll() : _roleManager.GetAll().Where(x => x.LoweredRoleName != "S").ToList();
                model.Message = "System error. "+ex.Message;
                model.IsSucceed = 0;
                return View("Edit", model);
            }
            
        }

       
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpGet]
        public ActionResult ResetPassword()
        {
            var model = new UserViewModel();
            return View("ResetPassword", model);

        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpPost]
        public ActionResult ResetPassword(UserViewModel model)
        {
            if (model.UserIdForReset != null)
            {
                model.User.UserName = model.UserIdForReset;
                ResponseModel = _userManager.ResetUserPassword(model.User);
                model.Message = ResponseModel.Message;
                model.IsSucceed = ResponseModel.ResponsStatus;
            }
            return View("ResetPassword", model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin,BranchUser")]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var model = new UserViewModel();
            return View("ChangePassword", model);
        }

        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpPost]
        public ActionResult ChangePassword(UserViewModel model)
        {
            if (model.User.Password != null)
            {
                string password = model.User.Password;
                if (IsAlphaNumeric(password) || IsRegularExpression(password))
                {
                    ResponseModel = _userManager.ChangePassword(model.OldPassword, model.User);
                    model.IsSucceed = ResponseModel.ResponsStatus;
                    model.Message = ResponseModel.Message;
                    if (model.IsSucceed == 1)
                    {
                        FormsAuthentication.SignOut();
                        Session.Clear();

                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    model.Message =
                        "Password must be combination of alphanumeric with An uppercase letter & a lowercase letter.";

                }

            }

            return View("ChangePassword", model);
        }

        public ActionResult ForgotPassword()
        {
            var model = new UserViewModel();
            return View("ForgotPassword", model);
        }
        //[Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpPost]
        public ActionResult ForgotPassword(UserViewModel model)
        {
            string UserN =  _userManager.FindOne(model.User.UserName).UserName;
            string UserPhone = _userManager.FindOne(model.User.UserName).PhoneNumber;
            if (UserN == model.User.UserName & UserPhone == model.User.PhoneNumber)
            {
                if (model.User.Password != null)
                {
                    string password = model.User.Password;
                    string oldPass = _userManager.FindOne(model.User.UserName).Password;
                    if (IsAlphaNumeric(password) || IsRegularExpression(password))
                    {
                        ResponseModel = _userManager.ChangePassword(oldPass, model.User);
                        model.IsSucceed = ResponseModel.ResponsStatus;
                        model.Message = ResponseModel.Message;
                        if (model.IsSucceed == 1)
                        {
                            //FormsAuthentication.SignOut();
                            //Session.Clear();

                            return RedirectToAction("Login", "Account");
                        }
                    }
                    else
                    {
                        model.Message =
                            "Password must be combination of alphanumeric with An uppercase letter & a lowercase letter.";

                    }

                }
            }
            else
            {
                model.Message ="User Id & Phone Number Not Match.";
            }
            return View("ChangePassword", model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public JsonResult GetDistrictByBankCode(string bankCode)
        {
            var districtList = _branchInfoManager.GetDistrictList(bankCode);
            return Json(districtList, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public JsonResult GetBranchNameByDistrict(string districtCode)
        {
            var brnchList = _branchInfoManager.GetBranchList(districtCode);
            return Json(brnchList, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public JsonResult GetAgentByBranch(string branchCode)
        {
            var agentList = _branchInfoManager.GetSubBranchInfoes(branchCode);
            return Json(agentList, JsonRequestBehavior.AllowGet);
        }
        public static bool IsAllLetters(string s)
        {
            return s.Any(Char.IsLetter);
        }
        public static bool IsAlphaNumeric(string s)
        {
            var rg = new Regex(@"^(?=.*[A-Za-z])(?=.*[0-9])[A-Za-z0-9]{8,30}$");
            return rg.IsMatch(s);
        }
        public static bool IsRegularExpression(string s)
        {
            var rg = new Regex(@"^(?=.*[!@#$%^&*()\-_=+`~\[\]{}?|])(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,20}$");
            return rg.IsMatch(s);
        }
    }
}