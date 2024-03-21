using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Office2010.Excel;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System.IO;

namespace RMS.Web.Controllers
{
    public class DemandController : Controller
    {
        private readonly IOrganizationManager _organizationManager;
        private readonly IDemandManager _demandManager;
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly IControlTypeManager _controlTypeManager;
        private readonly IProjectNoteManager _projectNoteManager;
        public DemandController(IProjectNoteManager projectNoteManager, IDemandManager demandManager, IOrganizationManager organizationManager, ICommonCodeManager commonCodeManager, IControlTypeManager controlTypeManager)
        {
            _projectNoteManager = projectNoteManager;
            _demandManager = demandManager;
            _organizationManager = organizationManager;
            _commonCodeManager = commonCodeManager;
            _controlTypeManager = controlTypeManager;
        }
        public ActionResult Index(DemandViewModel model)
        {
            model.Demands = _demandManager.GetAllForIndex().OrderBy(x=>x.IsAccept).ToList();
            return View(model);
        }

        public ActionResult ProjectCancel(string DemandId, DemandViewModel model)
        {
           int st = _demandManager.CancelStatusUpdate(DemandId);
           if (st == 1)
           {
               model.SuccessMessage = "Project Cancel successfully!";

           }
           else
           {
               model.FailedMessage = "Project Cancel failed!";
           }
            return RedirectToAction("Index",model);
        }
        public ActionResult ProjectUndo(string DemandId,DemandViewModel model)
        {
          int st = _demandManager.UndoStatusUpdate(DemandId);
          if (st == 1)
          {
              model.SuccessMessage = "Project Undo successfully!";

          }
          else
          {
              model.FailedMessage = "Project Undo failed!";
          }
          return RedirectToAction("Index", model);
        }
        public ActionResult SearchByKey(DemandViewModel model)
        {
            string searchKey = model.SearchKey;
         
                searchKey = searchKey.ToLower();
            model.Demands =
                _demandManager.GetSearchData(searchKey);
                if (!model.Demands.Any())
                {
                    //model.SuccessMessage = 0;
                    model.FailedMessage = "Data is not found.";
                }

                return View("Index", model);
        }
        [HttpGet]
        public ActionResult Create(long? DemandId, DemandViewModel model)
        {

            model.Demand = _demandManager.GetDemandById(DemandId) ?? new Demand();
           if(model.Demand  != null)
           {
               string dirFullPath = Server.MapPath(model.Demand.FileUrl);
               //HttpPostedFileBase imageUpload = HttpContext.Current.Server.MapPath(dirFullPath);
           }
            //model.ImageUpload = model.Demand.FileUrl;
            model.Departments = _organizationManager.GetAllOrganizations().Select(x => new SelectionList()
            {
                Value = x.OrganizationId,
                Text = x.OrganizationName
            }).ToList();
            model.ProjectType = _commonCodeManager.GetCommonCodeByType("Project Type").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            model.ApprovalType = _commonCodeManager.GetCommonCodeByType("Approval Authority").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            model.ProjectCategory = _commonCodeManager.GetCommonCodeByType("Project Category").Select(x => new SelectionList()
            {
                Value = x.CommonCodeID,
                Text = x.TypeValue
            }).ToList();
            model.YearCalender = _demandManager.GetYearCalender().Select(x => new SelectionStringList()
            {
                Value = x.Year,
                Text = x.Year
            }).ToList();
            model.ControlTypes = _controlTypeManager.GetControlType();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DemandViewModel model)
        {
            if (model != null)
            {
                Demand com = new Demand();
                com = _demandManager.GetDemandById(model.Demand.DemandId);

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.Demand.FileUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                model.Demand.DemandingUrl = ImageUpload.ImageUploadFile(model.DemandingUpload, dirFullPath, dirPath);
                if (com != null)
                {
                    com.DemandId = model.Demand.DemandId;
                    com.FileNo = model.Demand.FileNo;
                    com.OrganigationId = model.Demand.OrganigationId;
                    com.Category = model.Demand.Category;
                    com.ProjectType = model.Demand.ProjectType;
                    com.ProjectCat = model.Demand.ProjectCat;
                    com.ChildOrganization = model.Demand.ChildOrganization;
                    com.EndDate = model.Demand.EndDate;
                    com.FileUrl = model.Demand.FileUrl ?? com.FileUrl;
                    com.DemandingUrl = model.Demand.DemandingUrl ?? com.DemandingUrl;
                    com.ControllType = model.Demand.ControllType;
                    com.LastUpdateId = PortalContext.CurrentUser.UserName;
                    com.LastUpdateDate = DateTime.Now;
                    model.Demand = com;

                }
                else
                {
                    model.Demand.CreateId = PortalContext.CurrentUser.UserName;
                    model.Demand.CreateDate = DateTime.Now;
                }
                int result = _demandManager.Create(model.Demand);
                model = new DemandViewModel();
                if (result == 1)
                {
                    model.SuccessMessage = "Demand has been created successfully!";

                }
                else
                {
                    model.FailedMessage = "Demand creation failed!";
                }

            }

            return RedirectToAction("Create", model);
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
        public ActionResult ProjectNote(int? id,DemandViewModel model)
        {
            model.ProjectNotes = _projectNoteManager.GetAllbyId(id);
            return View(model);
        }
        public ActionResult ProjectNoteIndex(DemandViewModel model)
        {
            model.ProjectNotes = _projectNoteManager.GetAll();
            return View(model);
        }
        public ActionResult ProjectNoteSearch(DemandViewModel model)
        {
            string searchKey = model.SearchKey;

        
            model.ProjectNotes =
                _projectNoteManager.GetSearchValue(searchKey);
            if (!model.ProjectNotes.Any())
            {
                //model.SuccessMessage = 0;
                model.FailedMessage = "Data is not found.";
            }

            return View("ProjectNoteIndex", model);
        }
        public ActionResult ProjectNoteEdit(int? id,DemandViewModel model)
        {
     
                model.ProjectNote = _projectNoteManager.GetById(id) ?? new ProjectNote();
                model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
                {
                    Value = x.DemandId,
                    Text = x.FileNo
                }).ToList();
            return View(model);
        }
        public ActionResult ProjectNoteSave(DemandViewModel model)
        {
            if (model != null)
            {
                ProjectNote com = new ProjectNote();
                com = _projectNoteManager.GetById(model.ProjectNote.Id);
              
                if (com != null)
                {
                    com.DemandId = model.ProjectNote.DemandId;
                    com.Parameter = model.ProjectNote.Parameter;
                    com.Value = model.ProjectNote.Value;
                    com.Complete = model.ProjectNote.Complete;
           
                    com.LastUpdateId = PortalContext.CurrentUser.UserName;
                    com.LastUpdateDate = DateTime.Now;
                    model.ProjectNote = com;

                }
                else
                {
                    model.ProjectNote.CreateId = PortalContext.CurrentUser.UserName;
                    model.ProjectNote.CreateDate = DateTime.Now;
                }
                int result = _projectNoteManager.Create(model.ProjectNote);
           
                if (result == 1)
                {
                    model.SuccessMessage = "Note has been created successfully!";

                }
                else
                {
                    model.FailedMessage = "Note creation failed!";
                }

            }
            return RedirectToAction("ProjectNoteEdit", model );
        }
        public ActionResult ProjectNoteDelete(int? id,DemandViewModel model)
        {
            int st = _projectNoteManager.DeleteProjectNote(id);
            if (st == 1)
            {
                model.SuccessMessage = "Project Note Delete successfully!";

            }
            return RedirectToAction("ProjectNoteIndex");
        }
	}
}