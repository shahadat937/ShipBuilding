using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    public class AgendaController : BaseController
    {
        private readonly IAgendumManager _agendumManager;
        private readonly IDemandManager _demandManager;
        private readonly ICommonCodeManager _commonCodeManager;
        public AgendaController(IAgendumManager agendumManager, IDemandManager demandManager, ICommonCodeManager commonCodeManager)
        {
            _agendumManager = agendumManager;
            _demandManager = demandManager;
            _commonCodeManager = commonCodeManager;
        }
        public ActionResult Index(AjendaViewModel model)
        {
            model.Agendums = _agendumManager.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create(long? AjendaId, AjendaViewModel model)
        {

            model.Agendum = _agendumManager.GetAndaId(AjendaId) ?? new Agendum();
            model.CategoryLists = _commonCodeManager.GetCommonCodeByType("LetterCategory").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.AdditonalValue
            }).ToList();
            return View(model);
        }
        public ActionResult SearchByKey(AjendaViewModel model)
        {
            string searchKey = model.SearcKey;

            searchKey = searchKey.ToLower();
            model.Agendums =
                _agendumManager.GetSearchData(searchKey);
            if (!model.Agendums.Any())
            {
                //model.SuccessMessage = 0;
                model.FailedMessage = "Data is not found.";
            }

            return View("Index", model);
        }
        [HttpPost]
        public ActionResult Create(AjendaViewModel model)
        {
            if (model != null)
            {
                Agendum com = new Agendum();
                com = _agendumManager.GetAndaId(model.Agendum.Id);

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.Agendum.FileUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                if (com != null)
                {
                    com.Id = model.Agendum.Id;
                    com.FileNo = model.Agendum.FileNo;
                    com.Category = model.Agendum.Category;
                    com.Subject = model.Agendum.Subject;
                    com.Heading = model.Agendum.Heading;
                    com.FileUrl = model.Agendum.FileUrl ?? com.FileUrl;
                    com.Formula = model.Agendum.Formula;
                    com.Status = model.Agendum.Status;
                    com.StatusDate = model.Agendum.StatusDate;
                  
                    com.LastUpdateId = PortalContext.CurrentUser.UserName;
                    com.LastUpdateDate = DateTime.Now;
                    model.Agendum = com;

                }
                else
                {
                    model.Agendum.CreateId = PortalContext.CurrentUser.UserName;
                    model.Agendum.CreateDate = DateTime.Now;
                }
                int result = _agendumManager.Create(model.Agendum);

                if (result == 1)
                {
                    model.SuccessMessage = "Agenda has been created successfully!";

                }
                else
                {
                    model.FailedMessage = "Agenda creation failed!";
                }

            }

            return View(model);
        }

        // File Name Auto Complete 
        public JsonResult GetFileNameBySearchCharacter(string SearchCharacter)
        {

            var list = _demandManager.GetAll().Where(x => x.FileNo.ToLower().StartsWith(SearchCharacter.ToLower())).ToList();

            List<vwFileAutoCompleteData> fileInfoes = new List<vwFileAutoCompleteData>();
            foreach (var item in list)
            {
                vwFileAutoCompleteData fileInfo = new vwFileAutoCompleteData();
                fileInfo.FileNo = item.FileNo;
              
                fileInfoes.Add(fileInfo);
            }
            return Json(fileInfoes, JsonRequestBehavior.AllowGet);
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