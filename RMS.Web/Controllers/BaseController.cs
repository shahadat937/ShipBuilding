using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Controllers
{
   
    public abstract class BaseController : Controller
    {
        protected BaseController()
        {
            ResponseModel=new ResponseModel();
        }
        public ResponseModel ResponseModel { get; set; }
        internal bool Validate(object model)
        {
            try
            {
                ValidateModel(model);
            }
            catch { }

            return ModelState.IsValid;
        }

        internal List<string> CurrentErrors
        {
            get
            {
                return ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
        }

        public JsonResult ErrorResult()
        {
            return Json(new { Success = false, Errors = CurrentErrors });
        }

        public JsonResult ErrorResult(string message)
        {
            return Json(new { Success = false, Errors = message });
        }

        public JsonResult Reload()
        {
            return Json(new { Success = true, Reload = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Reload(int saveStatus)
        {
            return saveStatus > 0 ? Json(new { Success = true, Reload = true }, JsonRequestBehavior.AllowGet) : ErrorResult("Fail to save data");
        }

        public ActionResult Dialog()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }

            return View();
        }

        public ActionResult Dialog(object model)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }

        public ActionResult Dialog(string viewName, object model)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, model);
            }

            return View(viewName, model);
        }
    }
}
