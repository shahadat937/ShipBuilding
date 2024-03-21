using RMS.BLL.IManager;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    public class FileUploadFormsController : Controller
    {
        private readonly IDemandManager _iDemandManager;
        private readonly IFileUploadFormManager _iFileUploadFormManager;
        public FileUploadFormsController(IDemandManager iDemandManager, IFileUploadFormManager iFileUploadFormManager)
        {
            _iDemandManager = iDemandManager;
            _iFileUploadFormManager = iFileUploadFormManager;
        }
        public ActionResult Index()
        {
            FileUploadFormViewModel model = new FileUploadFormViewModel();
            model.FileUploadForms = _iFileUploadFormManager.GetAll();

            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            FileUploadFormViewModel model = new FileUploadFormViewModel();
            model.Projects = _iDemandManager.GetAll();
            return View(model);
        }
        [HttpPost, ActionName("Create")]
        public ActionResult ConfirmedCreate(FileUploadFormViewModel model)
        {
            if(model==null)
            {
                return HttpNotFound();
            }
            if (model.TE_AUpload != null)
            {
              
               
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.TEA = ImageUpload.ImageUploadFile(model.TE_AUpload, dirFullPath, dirPath);
            }
            if (model.FATScheduleUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.FATSchedule = ImageUpload.ImageUploadFile(model.FATScheduleUpload, dirFullPath, dirPath);
            }
            if (model.FATProcedureUpload != null)
            {
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.FATProcedure = ImageUpload.ImageUploadFile(model.FATProcedureUpload, dirFullPath, dirPath);
            }
            if (model.FATReportUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.FATReport = ImageUpload.ImageUploadFile(model.FATReportUpload, dirFullPath, dirPath);
            }
            if (model.HATSATUpload != null)
            {
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.HATSAT = ImageUpload.ImageUploadFile(model.HATSATUpload, dirFullPath, dirPath);
            }
            if (model.PITUpload != null)
            {
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.PIT = ImageUpload.ImageUploadFile(model.PITUpload, dirFullPath, dirPath);
            }
            if (model.LettergoUpload != null)
            {
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.LetterGo = ImageUpload.ImageUploadFile(model.LettergoUpload, dirFullPath, dirPath);
            }
            if (model.ApproveltrUpload != null)
            {
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.ApprovedGo = ImageUpload.ImageUploadFile(model.ApproveltrUpload, dirFullPath, dirPath);
            }
            model.Message = _iFileUploadFormManager.SaveFileUploadForms(model.FileUploadForm);
            model.Projects = _iDemandManager.GetAll();
            return View(model);
        }
        public ActionResult Show(string url)
        {
            try
            {
                if (url == null) return null;
                string dirFullPath = Server.MapPath(url);
                var fileStream = new FileStream(dirFullPath, FileMode.Open, FileAccess.Read);
                return new FileStreamResult(fileStream, "application/pdf");
            }
            catch (Exception ex)
            {
                TempData["message"] = "PDF File Not Found.";
                return View("_Blank");
            }
        }
        [HttpGet]
        public ActionResult Delete(string fileUploadFormId)
        {
            FileUploadFormViewModel model = new FileUploadFormViewModel();
            if(fileUploadFormId==null)
            {
                return HttpNotFound();
            }
            model.FileUploadForm = _iFileUploadFormManager.GetFileUploadForm(Convert.ToInt64(fileUploadFormId));
            if (model.FileUploadForm==null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(FileUploadFormViewModel model)
        {
            if(model.FileUploadForm.FileUploadFormId<=0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _iFileUploadFormManager.DeleteFileUploadForm(model.FileUploadForm.FileUploadFormId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int fileUploadFormId)
        {
            FileUploadFormViewModel model = new FileUploadFormViewModel();
            if (fileUploadFormId <0)
            {
                return HttpNotFound();
            }
            model.FileUploadForm = _iFileUploadFormManager.GetFileUploadFormByID(Convert.ToInt64(fileUploadFormId));
            if (model.FileUploadForm == null)
            {
                return HttpNotFound();
            }
            model.Projects = _iDemandManager.GetAll();
            return View(model);
        }
        [HttpPost,ActionName("Edit")]
        public ActionResult ConfirmedEdit(FileUploadFormViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            if (model.TE_AUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.TEA = ImageUpload.ImageUploadFile(model.TE_AUpload, dirFullPath, dirPath);
            }
            if (model.FATScheduleUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.FATSchedule = ImageUpload.ImageUploadFile(model.FATScheduleUpload, dirFullPath, dirPath);
            }
            if (model.FATProcedureUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.FATProcedure = ImageUpload.ImageUploadFile(model.FATProcedureUpload, dirFullPath, dirPath);
            }
            if (model.FATReportUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.FATReport = ImageUpload.ImageUploadFile(model.FATReportUpload, dirFullPath, dirPath);
            }
            if (model.HATSATUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.HATSAT = ImageUpload.ImageUploadFile(model.HATSATUpload, dirFullPath, dirPath);
            }
            if (model.PITUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.PIT = ImageUpload.ImageUploadFile(model.PITUpload, dirFullPath, dirPath);
            }
            if (model.LettergoUpload != null)
            {
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.LetterGo = ImageUpload.ImageUploadFile(model.LettergoUpload, dirFullPath, dirPath);
            }
            if (model.ApproveltrUpload != null)
            {
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.FileUploadForm.ApprovedGo = ImageUpload.ImageUploadFile(model.ApproveltrUpload, dirFullPath, dirPath);
            }
            model.Message = _iFileUploadFormManager.SaveFileUploadForms(model.FileUploadForm);
            model.Projects = _iDemandManager.GetAll();
            return View(model);
        }
    }
}