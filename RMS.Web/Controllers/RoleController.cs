using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        private IRoleManager _roleManager;
        public RoleController(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }
         [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult Index(RoleViewModel model)
        {
            List<Role> response = _roleManager.GetAll();
            if (response.Any())
            {
                model.Roles = response;
            }
            else
            {
                model.Message = "Data is not found.";
            }
            return View(model);
        }
         [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult Edit(string RoleId, RoleViewModel model)
        {
            if (RoleId != null)
            {
                model.Role = _roleManager.FindOne(Convert.ToInt16(RoleId));

            }
            else
            {
                model.Role = new Role();
            }
            return View(model);
        }
    }
}
