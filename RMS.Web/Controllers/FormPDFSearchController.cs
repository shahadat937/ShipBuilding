using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RMS.Web.Controllers
{
    public class FormPDFSearchController : Controller
    {
        private readonly IFormInfoManager _iFormInfoManager;
        private readonly IStaffRequirementManager _iStaffRequirementManager;
        private readonly ITenderSpecOpinionManager _iTenderSpecOpinionManager;
        private readonly IDemandManager _iDemandManager;
        public FormPDFSearchController(IFormInfoManager iFormInfoManager, IStaffRequirementManager iStaffRequirementManager,
            ITenderSpecOpinionManager iTenderSpecOpinionManager, IDemandManager iDemandManager)
        {
            _iFormInfoManager = iFormInfoManager;
            _iStaffRequirementManager = iStaffRequirementManager;
            _iTenderSpecOpinionManager = iTenderSpecOpinionManager;
            _iDemandManager = iDemandManager;
        }
        [HttpGet]
        public ActionResult Index(string id, string fileName)
        {
            FormInfoViewModel model = new FormInfoViewModel();
            model.FormInfoes = _iFormInfoManager.GetSpecificFormInfoById(id, fileName);
            if (fileName==null)
            {
                model.Message = "To get related data,Please enter a keyword to search ";
                return PartialView("_dynamicFileInfo", model);
            }
            else
            { 
                return PartialView("_dynamicFileInfo", model);
            }
        }
        public ActionResult GetForms()
        {
            FormInfoViewModel model = new FormInfoViewModel();
            model.FormInfoes = _iFormInfoManager.GetAll();
            return View(model);
        }
        public ActionResult Show(string url)
        {
            try
            {
                if (url == null) return null;
                var fileStream = new FileStream(url, FileMode.Open, FileAccess.Read);
                return new FileStreamResult(fileStream, "application/pdf");
            }
            catch (Exception ex)
            {
                TempData["message"] = "PDF File Not Found.";
                return View("_Blank");
            }
        }
    }
}