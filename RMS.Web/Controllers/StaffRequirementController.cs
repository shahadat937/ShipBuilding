using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
//using DocumentFormat.OpenXml.Drawing;
using iTextSharp.text.pdf.qrcode;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System.IO;

namespace RMS.Web.Controllers
{
    public class StaffRequirementController : Controller
    {
        private readonly IStaffRequirementManager _staffRequirementManager;
        private readonly IControlTypeManager _controlTypeManager;
        private readonly IStatusManager _statusManager;
        public readonly IAgendumManager _AgendumManager;
        private readonly IFlowListManager _flowListManager;
        private readonly ICommitteNameListManager _committeNameListManager;
        private readonly IFlowSetupManager _flowSetupManager;
        private readonly IDemandManager _demandManager;
        public StaffRequirementController(IStaffRequirementManager staffRequirementManager, IControlTypeManager controlTypeManager, IStatusManager statusManager, IAgendumManager AgendumManager, IFlowListManager flowListManager, ICommitteNameListManager committeNameListManager, IFlowSetupManager flowSetupManager, IDemandManager demandManager)
        {
            _staffRequirementManager = staffRequirementManager;
            _controlTypeManager = controlTypeManager;
            _statusManager = statusManager;
            _AgendumManager = AgendumManager;
            _flowListManager = flowListManager;
            _committeNameListManager = committeNameListManager;
            _flowSetupManager = flowSetupManager;
            _demandManager = demandManager;
        }
        public ActionResult Index( StaffRequirementViewModel model,string projectId,string flowserial)
        {
            model.StaffRequirements = _staffRequirementManager.GetStaffRequirement(projectId);
            model.StaffRequirement.FileNo = projectId != null ? Convert.ToInt64(projectId): 0;
            model.FlowSerial = flowserial;
            return View(model);
        }
        public ActionResult Acceptance(StaffRequirementViewModel model, string projectId)
        {
            model.StaffRequirements = _staffRequirementManager.GetStaffRequirement(projectId).Where(x => x.Status == 1).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create(long? fileId,string flowserial, StaffRequirementViewModel model)
        {
            model.Statuses = _statusManager.GetStatus();
            model.FlowSerial = flowserial;
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.CommitteList = _committeNameListManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.Id,
                Text = x.CommitteName
            }).ToList();
            model.StaffRequirement = _staffRequirementManager.GetStaffRequirementByFile(fileId) ?? new StaffRequirement();
            model.StaffRequirement.FileNo = fileId;
            return View(model);
         
        }

       [HttpGet]
        public ActionResult StaffRequirementApproveIndex(long? fileId, string flowserial, string StuffRequirementId, StaffRequirementViewModel model)
        {
      
            model.Statuses = _statusManager.GetStatus();
            model.FlowSerial = flowserial;
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.CommitteList = _committeNameListManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.Id,
                Text = x.CommitteName
            }).ToList();
            model.StaffRequirement = _staffRequirementManager.GetStaffRequirementByFile(fileId) ?? new StaffRequirement();
            model.StaffRequirement.FileNo = fileId;
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateValue(StaffRequirementViewModel model)
        {
            if (model != null)
            {
                int result;
                StaffRequirement sr = new StaffRequirement();
                sr = _staffRequirementManager.GetStaffRequirementById(model.StaffRequirement.StuffRequirementId);
                if (model.ImageUpload != null)
                {

                    string dirPath = @"~/File";
                    string dirFullPath = Server.MapPath(dirPath);
                    model.StaffRequirement.FileUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                }
                if (model.ApproveDocument != null)
                {

                    string dirPath = @"~/File";
                    string dirFullPath = Server.MapPath(dirPath);
                    model.StaffRequirement.ApproveFile = ImageUpload.ImageUploadFile(model.ApproveDocument, dirFullPath, dirPath);
                }

       
                if (sr != null)
                {
                    _flowSetupManager.UpdateFormStatusInFlow(model.StaffRequirement.FileNo, model.FlowSerial, FormInformation.StaffRequirement,null);
          
                    
                    sr.FileNo = model.StaffRequirement.FileNo;
                    sr.Title = model.StaffRequirement.Title;
                    sr.Status = model.StaffRequirement.Status;
                    sr.CommitteID = model.StaffRequirement.CommitteID;
                    sr.CreateDate = model.StaffRequirement.CreateDate;
                    sr.Remarks = model.StaffRequirement.Remarks;
                    sr.FileUrl = model.StaffRequirement.FileUrl ?? sr.FileUrl;
                    sr.ApproveFile = model.StaffRequirement.ApproveFile ?? sr.ApproveFile;
                    sr.IsApprove = model.StaffRequirement.IsApprove;
                    sr.UpdatedBy = PortalContext.CurrentUser.UserName;
                    sr.LastUpdate = DateTime.Now;
                   // model.StaffRequirement = sr;
                   result = _staffRequirementManager.Create(sr);
                   if (result == 1)
                   {
                       model.SuccessMessage = "Staff Requirement has been created successfully!";

                   }
                   else
                   {
                       model.FailedMessage = "StaffRequirement creation failed!";
                   }
                }
                else
                {
                    // Flow Url Status Update 
                    _flowSetupManager.UpdateFormStatusInFlow(model.StaffRequirement.FileNo, model.FlowSerial, FormInformation.StaffRequirement, DateTime.Now);
                    model.StaffRequirement.UserId = PortalContext.CurrentUser.UserName;
                    model.StaffRequirement.UpdatedBy = PortalContext.CurrentUser.UserName;
                    model.StaffRequirement.LastUpdate = DateTime.Now;
                     result = _staffRequirementManager.Create(model.StaffRequirement);
                    if (result == 1)
                    {
                        model.SuccessMessage = "Staff Requirement has been created successfully!";

                    }
                    else
                    {
                        model.FailedMessage = "StaffRequirement creation failed!";
                    }
                }
            }
            model.Statuses = _statusManager.GetStatus();
      
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.CommitteList = _committeNameListManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.Id,
                Text = x.CommitteName
            }).ToList();
            return View("Create", model);
        }
        [HttpPost]
        public ActionResult StaffRequirementApproveUpdate(StaffRequirementViewModel model)
        {
            if (model != null)
            {
                StaffRequirement sr = new StaffRequirement();
                sr = _staffRequirementManager.GetStaffRequirementById(model.StaffRequirement.StuffRequirementId);
                if (model.ImageUpload != null)
                {

                    string dirPath = @"~/File";
                    string dirFullPath = Server.MapPath(dirPath);
                    model.StaffRequirement.FileUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                }
                if (model.ApproveDocument != null)
                {

                    string dirPath = @"~/File";
                    string dirFullPath = Server.MapPath(dirPath);
                    model.StaffRequirement.ApproveFile = ImageUpload.ImageUploadFile(model.ApproveDocument, dirFullPath, dirPath);
                }


                if (sr != null)
                {
                    _flowSetupManager.UpdateFormStatusInFlow(model.StaffRequirement.FileNo, model.FlowSerial, FormInformation.StaffRequirementApprove,DateTime.Now);


                    sr.FileNo = model.StaffRequirement.FileNo;
                    sr.Title = model.StaffRequirement.Title;
                    sr.ApproveFile = model.StaffRequirement.ApproveFile ?? sr.ApproveFile;
                    if (model.StaffRequirement.IsApprove != true)
                    {
                        sr.IsApprove = model.StaffRequirement.IsApprove;
                        sr.ApproveDate =null;
                        sr.ApproveRemarks = null;
                    }
                    else
                    {
                        sr.Reference = model.StaffRequirement.Reference;
                        sr.Decition = model.StaffRequirement.Decition;
                        sr.IsApprove = model.StaffRequirement.IsApprove;
                        sr.ApproveDate = model.StaffRequirement.ApproveDate;
                        sr.ApproveRemarks = model.StaffRequirement.ApproveRemarks;
                    }

                    int result = _staffRequirementManager.Create(sr);

                    if (result == 1)
                    {
                        model.SuccessMessage = "Approved Information Created successfully!";
                    }
                    else
                    {
                        model.FailedMessage = "Approved Information Create failed!";
                    }
                }
            
            }
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.CommitteList = _committeNameListManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.Id,
                Text = x.CommitteName
            }).ToList();
            return View("StaffRequirementApproveIndex", model);
        }
        [HttpGet]
        public ActionResult Accept(int st,string StuffRequirementId,StaffRequirementViewModel model,string projectId)
        {
            model.StaffRequirement.Status = 12; 
            //}
            if (StuffRequirementId != null)
            {
                StaffRequirement sr = new StaffRequirement();
                StaffRequirement sri = _staffRequirementManager.GetStaffRequirementById(Convert.ToInt64(StuffRequirementId));
                
               
                sri.Status = Convert.ToByte(st);
               // sr.EntryDate = sri.EntryDate;
                sri.LastUpdate = DateTime.Now;
                int result = _staffRequirementManager.Accept(sri);

                if (result == 1)
                {
                    model.SuccessMessage = "StaffRequirement has been Update successfully!";

                }
                else
                {
                    model.FailedMessage = "StaffRequirement creation failed!";
                }
                model.StaffRequirements = _staffRequirementManager.GetStaffRequirement(projectId).Where(x => x.Status == 1).ToList();
                
            }

            return View("Acceptance",model);
        }
        //public ActionResult SearchByKey(StaffRequirementViewModel model)
        //{
        //    string searchKey = model.StaffRequirement.Title;
        //    if (searchKey == null)
        //    {
        //        model.StaffRequirements = _staffRequirementManager.GetStaffRequirement();
        //    }
        //    else
        //    {
        //        searchKey = searchKey.ToLower();
        //        model.StaffRequirements = _staffRequirementManager.GetStaffRequirement().Where(x => x.Title.ToLower().Contains(searchKey)).ToList();
        //        if (!model.StaffRequirements.Any())
        //        {
        //            //model.SuccessMessage = 0;
        //            model.FailedMessage = "Data is not found.";
        //        }
        //    }
        //    return View("Index", model);
        //}
        public ActionResult SearchByKey(StaffRequirementViewModel model, string projectId)
        {
            
            if (model.SearchKey == null)
            {
                model.FailedMessage = "Please enter a keyword to search";
                model.StaffRequirements = _staffRequirementManager.GetStaffRequirement(projectId);
            }
            else
            {
                string searchKey = model.SearchKey.ToLower();
                model.StaffRequirements = _staffRequirementManager.GetStaffRequirementsBySearchKey(searchKey);
                if (!model.StaffRequirements.Any())
                {
                    //model.SuccessMessage = 0;
                    model.FailedMessage = "Data is not found with this " + "'" + searchKey + "'";
                }
            }
            return View("Index", model);
        }

        // File Name Auto Complete 
        public JsonResult GetFileNameBySearchCharacter(string SearchCharacter)
        {

            var list = _AgendumManager.GetAll().Where(x => x.FileNo.ToLower().StartsWith(SearchCharacter.ToLower())).ToList();

            List<vwFileAutoCompleteData> fileInfoes = new List<vwFileAutoCompleteData>();
            foreach (var item in list)
            {
                vwFileAutoCompleteData fileInfo = new vwFileAutoCompleteData();
                fileInfo.FileNo = item.FileNo;
                fileInfo.Subject = item.Subject;
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