using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class CommonCodeController : Controller
    {

        private readonly ICommonCodeManager _commonCode;
        public CommonCodeController(ICommonCodeManager CommonCodeManager)
        {
            _commonCode = CommonCodeManager;

        }
        [Authorize(Roles = "UserManagement,AdminUser")]
        public ActionResult Index(CommonCodeViewModel model)
        {
            //Type = model.Type;
         
            model.CommonCode = _commonCode.GetCommonCode();
            model.GetCommonCodeByType = _commonCode.GetCommonType();
            ModelState.Clear();
            if (model.Code != null)
            {
                CommonCode CC = _commonCode.FindOne(Convert.ToInt32(model.Code));
                model.Code = CC.Code;
                model.Type = CC.Type;
                model.TypeValue = CC.TypeValue;
                model.DisplayCode = CC.DisplayCode;
                model.Status = CC.Status;
                model.CommonCode.Add(CC);

            }
            model.CommonCodeTreeViews = _commonCode.GetCommonCodeTeeView(model.Type);
            return View(model);
        }
        public ActionResult Save(CommonCodeViewModel model)
        {
            //string ty = "?Type=";
      
            string BankCode = PortalContext.CurrentUser.BankCode;
            ResponseModel ResponseModel = new ResponseModel();
            ResponseModel = _commonCode.SaveCommonCode(model);
            if (ResponseModel.ResponsStatus > 0)
            {
                TempData["message"] = ResponseModel.Message;
                return RedirectToAction("Index", new { Type = model.Type });
            }
            return View("Index", model);
        }
        public ActionResult Delete(CommonCodeViewModel model)
        {
            if (model.Code != null)
            {
                int messeage = _commonCode.DeleteType(model.Code, model.Type);

                TempData["message"] = "Common Code Successfully Deleted.";
                return RedirectToAction("Index", new { Type = model.Type });
            }
            return View("Index", model);
        }


    }
}