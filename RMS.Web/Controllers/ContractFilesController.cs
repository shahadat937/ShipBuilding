using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    public class ContractFilesController : Controller
    {
        private readonly IContractFileManager _iContractFileManager;
        private readonly IDemandManager _iDemandManager;
        private readonly IFlowSetupManager _flowSetupManager;
        public ContractFilesController(IContractFileManager iContractFileManager, IDemandManager iDemandManager, IFlowSetupManager flowSetupManager)
        {
            _iContractFileManager = iContractFileManager;
            _iDemandManager = iDemandManager;
            _flowSetupManager = flowSetupManager;
        }
        public ActionResult Index(ContractFileViewModel model)
        {
            List<ContractFile> contractFiles = _iContractFileManager.GetAllContractFiles();
            foreach (var item in contractFiles)
            {
                item.DemandName = _iDemandManager.GetProjectNameById(Convert.ToInt64(item.DemandId));
                if (item.IsApproved == true)
                { item.Approved = "Approved"; }
                else { item.Approved = "Not Approved"; }

                model.ContractFiles.Add(item);
            }
            return View(model);
        }
        public ActionResult SearchByKey(ContractFileViewModel model)
        {
            if (model.SearchKey == null)
            {
                model.Message = "Sorry ,please search by giving data ";
                return View("Index", model);
            }
            string searchKey = model.SearchKey;
            searchKey = searchKey.ToLower();
            List<ContractFile> contractFiles = _iContractFileManager.GetContractFilesBySearchKey(searchKey);
            foreach (var item in contractFiles)
            {
                item.DemandName = _iDemandManager.GetProjectNameById(Convert.ToInt64(item.DemandId));
                if (item.IsApproved == true)
                { item.Approved = "Approved"; }
                else { item.Approved = "Not Approved"; }

                model.ContractFiles.Add(item);
            }
            if (!model.ContractFiles.Any())
            {
                model.Message = "Not exists";
            }
            return View("Index", model);
        }
        [HttpGet]
        public ActionResult Create(string fileId, string flowSerial, ContractFileViewModel model)
        {
            long fileID = Convert.ToInt64(fileId);
            ContractFile contractFile = _iContractFileManager.GetContractFileByDemandId(fileID);
            if (contractFile != null)
            {
                model.ContractFile = contractFile;
            }
            model.DemandSelectedItems = _iDemandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.FileSerial = flowSerial;
            model.ContractFile.DemandId = fileID;

            return View(model);
        }
     [HttpPost]
        public ActionResult Create(ContractFileViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            ContractFile contractFile = _iContractFileManager.GetContractFileByDemandId(model.ContractFile.DemandId);
            if (model.ImageUpload != null)
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.ContractFile.FileUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
            }
            if (model.ApproveDocument != null)
            {
                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.ContractFile.ApprovalFileUrl = ImageUpload.ImageUploadFile(model.ApproveDocument, dirFullPath, dirPath);
            }
            if (contractFile != null)
            {
              
                contractFile.DemandId = model.ContractFile.DemandId;
                contractFile.FileName = model.ContractFile.FileName;
                contractFile.IssueDate = model.ContractFile.IssueDate;
                contractFile.Remarks = model.ContractFile.Remarks;
                contractFile.FileUrl = model.ContractFile.FileUrl ?? contractFile.FileUrl;
                contractFile.UpdatedBy = PortalContext.CurrentUser.UserName;
                contractFile.UpdatedDate = DateTime.Now;
                model.ContractFile = contractFile;
                _flowSetupManager.UpdateFormStatusInFlow(model.ContractFile.DemandId, model.FileSerial, FormInformation.ContractFile,null);
            }
            else
            {
               
                model.ContractFile.CreatedBy = PortalContext.CurrentUser.UserName;
                model.ContractFile.CreatedDate = DateTime.Now;
                model.ContractFile.UpdatedBy = PortalContext.CurrentUser.UserName;
                model.ContractFile.UpdatedDate = DateTime.Now;
                _flowSetupManager.UpdateFormStatusInFlow(model.ContractFile.DemandId, model.FileSerial, FormInformation.ContractFile,DateTime.Now);
               
            }
            model.Message = _iContractFileManager.SaveContractFile(model.ContractFile);
            model.DemandSelectedItems = _iDemandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            return View("Create", model);
        }
         [HttpGet]
         public ActionResult ContractApproveIndex(string fileId, string flowSerial, ContractFileViewModel model)
         {
             long fileID = Convert.ToInt64(fileId);
             ContractFile contractFile = _iContractFileManager.GetContractFileByDemandId(fileID);
             if (contractFile != null)
             {
                 model.ContractFile = contractFile;
             }
             model.DemandSelectedItems = _iDemandManager.GetAll().Select(x => new SelectionList()
             {
                 Value = x.DemandId,
                 Text = x.FileNo
             }).ToList();
             model.FileSerial = flowSerial;
             model.ContractFile.DemandId = fileID;

             return View(model);
         }
         [HttpPost]
         public ActionResult ContractFileUpdate(ContractFileViewModel model)
         {
             if (model == null)
             {
                 return HttpNotFound();
             }
             ContractFile contractFile = _iContractFileManager.GetContractFileByDemandId(model.ContractFile.DemandId);
    
             if (model.ApproveDocument != null)
             {

                 string dirPath = @"~/File";
                 string dirFullPath = Server.MapPath(dirPath);
                 model.ContractFile.ApprovalFileUrl = ImageUpload.ImageUploadFile(model.ApproveDocument, dirFullPath, dirPath);
             }
             if (contractFile != null)
             {
                 if (model.ContractFile.IsApproved != true)
                 {
                     contractFile.ApproveRemarks = null;
                     contractFile.ApprovalFileUrl = null;
                     contractFile.ApproveDate = null;
                 }
                 else
                 {
                     _iDemandManager.UpdateProjectCompleteStatus(model.ContractFile.DemandId);
                     contractFile.DemandId = model.ContractFile.DemandId;
                     contractFile.Reference = model.ContractFile.Reference;
                     contractFile.Decition = model.ContractFile.Decition;
                     contractFile.ApproveDate = model.ContractFile.ApproveDate;
                     contractFile.ApproveRemarks = model.ContractFile.ApproveRemarks;
                     contractFile.IsApproved = model.ContractFile.IsApproved;
                     contractFile.ApprovalFileUrl = model.ContractFile.ApprovalFileUrl ?? contractFile.ApprovalFileUrl;
                 }
        
                 _flowSetupManager.UpdateFormStatusInFlow(model.ContractFile.DemandId, model.FileSerial, FormInformation.ContractFileApprove,DateTime.Now);
                 model.Message = _iContractFileManager.SaveContractFile(contractFile);
             }

             model.DemandSelectedItems = _iDemandManager.GetAll().Select(x => new SelectionList()
             {
                 Value = x.DemandId,
                 Text = x.FileNo
             }).ToList();
             return View("ContractApproveIndex", model);
         }
        [HttpGet]
        public ActionResult Delete(string id, ContractFileViewModel model)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            ContractFile contractFile = _iContractFileManager.GetContractFileById(Convert.ToInt64(id));
            if (contractFile == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.ContractFile = contractFile;

            contractFile.DemandName = _iDemandManager.GetProjectNameById(Convert.ToInt64(contractFile.DemandId));
            if (contractFile.IsApproved == true)
            { contractFile.Approved = "Approved"; }
            else { contractFile.Approved = "Not Approved"; }

            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(ContractFileViewModel model)
        {
            if (ModelState.IsValid)
            {
                ContractFile contractFile = new ContractFile();
                contractFile = _iContractFileManager.GetContractFileById(Convert.ToInt64(model.ContractFile.ContractFileId));
                if (contractFile != null)
                {
                    _iContractFileManager.DeleteContractFile(contractFile);
                    return RedirectToAction("Index");
                }
                else
                {
                    model.Message = "Data is not found.";
                }
            }
            return View();
        }

        //public List<SelectListItem> GetDemandNames()
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    List<Demand> demands = _iDemandManager.GetAll();
        //    demands.ForEach(x => items.Add(new SelectListItem() { Text = x.FileNo, Value = x.DemandId.ToString() }));
        //    return items;
        //}
    }
}