using System;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class UserURLRightController : Controller
    {
        private IUserURLRightManager _userURLRightManager;
        private IRoleManager _roleManager;

        public UserURLRightController(IUserURLRightManager userURLRightManager, IRoleManager roleManager)
        {
            _userURLRightManager = userURLRightManager;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpGet]
        public ActionResult Index(string roleName, UserURLRightViewModel model)
        {
            ModelState.Clear();
            model.Roles = PortalContext.CurrentUser.LoweredRoleName == "S" ? _roleManager.GetAll() : _roleManager.GetAll().Where(x => x.LoweredRoleName != "S").ToList();
            if (roleName != null)
            {
                model.UserURLRights = _userURLRightManager.GetUserRighsByRole(roleName);
                model.UserUrlRight.UserId = roleName;
            }
            return View(model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        [HttpPost]
        public ActionResult UpdateUserRights(UserURLRightViewModel model)
        {
            {
                ResponseModel response = _userURLRightManager.Save(model.UserURLRights);
                model.Message = response.Message;
                model.IsSucceed = response.ResponsStatus;
                model.Roles = _roleManager.GetAll();
                model.UserUrlRight.UserId = model.UserURLRights.FirstOrDefault().UserId;
                return View("Index", model);
            }
        }
    }
}