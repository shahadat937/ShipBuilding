using System.IO;
using System.Net;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RMS.Web.Controllers
{
    public class CommitteeRaiseLettersController : Controller
    {
        private readonly ICommitteeRaiseLetterManager _iCommitteeRaiseLetterManager;
        private readonly ICommonCodeManager _iCommonCodeManager;
        private readonly IFlowSetupManager _flowSetupManager;
        private readonly IDemandManager _demandManager;
        private readonly IFinanceMinistryLetterManager _financeMinistryLetter;
        private readonly IAFDLetterManager _afdLetterManager;
        private readonly IAFDLetterApproveManager _afdLetterApproveManager;
        private readonly IFileBackUpManager _fileBackUpManager;
        public CommitteeRaiseLettersController(ICommitteeRaiseLetterManager iCommitteeRaiseLetterManager, ICommonCodeManager iCommonCodeManager, IFlowSetupManager flowSetupManager, IDemandManager demandManager, IFinanceMinistryLetterManager financeMinistryLetter, IAFDLetterManager afdLetterManager, IAFDLetterApproveManager afdLetterApproveManager, IFileBackUpManager fileBackUpManager)
        {
            _iCommitteeRaiseLetterManager = iCommitteeRaiseLetterManager;
            _iCommonCodeManager = iCommonCodeManager;
            _flowSetupManager = flowSetupManager;
            _demandManager = demandManager;
            _financeMinistryLetter = financeMinistryLetter;
            _afdLetterManager = afdLetterManager;
            _afdLetterApproveManager = afdLetterApproveManager;
            _fileBackUpManager = fileBackUpManager;
        }
        public ActionResult Index(string formId,string flowserial, string proId, CommitteeRaiseLetterViewModel model)
        {
            model.FormId = formId;
            model.FlowSerial = flowserial;
            if (proId != null) model.CommitteeRaiseLetter.FileNo = Convert.ToInt64(proId); 

            switch (formId)
            {
                case "10010":
                    List<AFDLetter> afdLetters = _afdLetterManager.GetAfdLetterByProject(proId);
                       foreach (var afdlet in afdLetters)
                    {
                        afdlet.CommonCodeTypeValue = _iCommonCodeManager.GetCommonCodeTypeValueById(afdlet.CommonCodeTypeValueId);
                        model.AFDLetters.Add(afdlet);
                    }
                    model.FormName = "Letter send to AFD";
              
                    break;

                case "10016":
                    List<FinanceMinistryLetter> financeMinistryLetters = _financeMinistryLetter.GetFinanceMinistryByProject(proId);
                    foreach (var financeministry in financeMinistryLetters)
                    {
                        financeministry.CommonCodeTypeValue = _iCommonCodeManager.GetCommonCodeTypeValueById(financeministry.CommonCodeTypeValueId);
                        model.FininMinistryLetters.Add(financeministry);
                    }
                    model.FormName = "Letter send to Finance Ministry";
                    break;
                case "10017":
                    List<CommitteeRaiseLetter> committeeRaiseLetters = _iCommitteeRaiseLetterManager.GetCommitteeRaiseLettersbyProId(proId);
                    foreach (var committeeRaiseLetter in committeeRaiseLetters)
                    {
                       committeeRaiseLetter.CommonCodeTypeValue = _iCommonCodeManager.GetCommonCodeTypeValueById(committeeRaiseLetter.CommonCodeTypeValueId);
                        model.CommitteeRaiseLetters.Add(committeeRaiseLetter);
                    }
                    model.FormName = "Letter send to SFC";
                    break;
                //default:

            }
            return View(model);
        }
        public ActionResult SearchByKey(CommitteeRaiseLetterViewModel model)
        {
            if (model.SearchKey == null)
            {
                model.Message = "Sorry ,please search by giving data ";
                return View("Index", model);
            }
            string searchKey = model.SearchKey;
            List<CommitteeRaiseLetter> committeeRaiseLetters = _iCommitteeRaiseLetterManager.GetCommitteeRaiseLettersBySearchKey(searchKey);
            foreach (var committeeRaiseLetter in committeeRaiseLetters)
            {
                //committeeRaiseLetter.CommonCodeTypeValue = _iCommonCodeManager.GetCommonCodeTypeValueById(committeeRaiseLetter.CommonCodeTypeValueId);
                model.CommitteeRaiseLetters.Add(committeeRaiseLetter);
            }
            return View("Index", model);

        }
        [HttpGet]
        public ActionResult Create(string fileId, string flowserial, string formId, CommitteeRaiseLetterViewModel model)
        {
            switch (formId)
            {
                case "10010":
                    model.AfdLetter = _afdLetterManager.GetAFDLetterByFile(fileId) ?? new AFDLetter();

                    model.CommitteeRaiseLetter.CommitteeRaiseLetterId = model.AfdLetter.LetterId;
                    model.CommitteeRaiseLetter.FileNo = model.AfdLetter.FileNo;
                    model.CommitteeRaiseLetter.CommonCodeTypeValueId = model.AfdLetter.CommonCodeTypeValueId;
                    model.CommitteeRaiseLetter.IssueDate = model.AfdLetter.IssueDate;
                    model.CommitteeRaiseLetter.AttachedDocument = model.AfdLetter.AttachedDocument;
                 
                    model.CommitteeRaiseLetter.FromAuthority = model.AfdLetter.FromAuthority;
                    model.CommitteeRaiseLetter.ToAuthority = model.AfdLetter.ToAuthority;
                    model.CommitteeRaiseLetter.InformedAuthority = model.AfdLetter.InformedAuthority;
                    model.CommitteeRaiseLetter.Remarks = model.AfdLetter.Remarks;
                    model.FormName = "Letter send to AFD";
                break;
                case "10016":
                    model.FinanceMinistryLetter = _financeMinistryLetter.GetFinanceMinistryLetterByFile(fileId) ?? new FinanceMinistryLetter();

                    model.CommitteeRaiseLetter.CommitteeRaiseLetterId = model.FinanceMinistryLetter.LetterId;
                    model.CommitteeRaiseLetter.FileNo = model.FinanceMinistryLetter.FileNo;
                    model.CommitteeRaiseLetter.CommonCodeTypeValueId = model.FinanceMinistryLetter.CommonCodeTypeValueId;
                    model.CommitteeRaiseLetter.IssueDate = model.FinanceMinistryLetter.IssueDate;
                    model.CommitteeRaiseLetter.AttachedDocument = model.FinanceMinistryLetter.AttachedDocument;
                    model.CommitteeRaiseLetter.FromAuthority = model.FinanceMinistryLetter.FromAuthority;
                    model.CommitteeRaiseLetter.ToAuthority = model.FinanceMinistryLetter.ToAuthority;
                    model.CommitteeRaiseLetter.InformedAuthority = model.FinanceMinistryLetter.InformedAuthority;
                    model.CommitteeRaiseLetter.IsApprove = model.FinanceMinistryLetter.IsApprove;
                    model.CommitteeRaiseLetter.ApproveUrl = model.FinanceMinistryLetter.ApproveUrl;
                    model.CommitteeRaiseLetter.Remarks = model.FinanceMinistryLetter.Remarks;
                    model.FormName = "Letter send to Finance Ministry";
                    break;
                case "10017":
                        model.CommitteeRaiseLetter = _iCommitteeRaiseLetterManager.GetCommitteeRaiseLetterByFile(fileId) ?? new CommitteeRaiseLetter();
                        model.FormName = "Letter send to SFC";
                    break;
            }
            model.FormId = formId;
            model.FlowSerial = flowserial;
            if (fileId != null) model.CommitteeRaiseLetter.FileNo = Convert.ToInt64(fileId);

            model.CommonCodeTypeValues = CommonCodeTypeValueSelectedList();
            model.ProjectLists = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();

            return View(model);
        }
        [HttpPost, ActionName("Create")]
        public ActionResult SaveCreate(CommitteeRaiseLetterViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }

            // Flow Url Status Update 
   
            string dirPath = @"~/File";
            string dirFullPath = Server.MapPath(dirPath);
            model.Message = _iCommitteeRaiseLetterManager.SaveCommitteeRaiseLetter(model.CommitteeRaiseLetter, dirFullPath,model.FlowSerial,model.FormId);
            model.CommonCodeTypeValues = CommonCodeTypeValueSelectedList();
            model.ProjectLists = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();

            switch (model.FormId)
            {
                case "10010":
                    model.FormName = "Letter send to AFD";

                    break;

                case "10016":
                    model.FormName = "Letter send to Finance Ministry";
                    break;
                case "10017":
                 
                    model.FormName = "Letter send to SFC";
                    break;
            }
            return View("Create", model);
        }
        public List<SelectListItem> CommonCodeTypeValueSelectedList()
        {
            List<SelectListItem> commonCodeTypeValueSelectedItems = new List<SelectListItem>();
            List<CommonCode> commonCodeList = _iCommonCodeManager.GetCommonCodeByType("RaiseLetterType");
            commonCodeList.ForEach(x => commonCodeTypeValueSelectedItems.Add(new SelectListItem() { Text = x.TypeValue, Value = x.CommonCodeID.ToString() }));
            return commonCodeTypeValueSelectedItems;
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            long CommitteeRaiseLetterId = Convert.ToInt64(id);
            CommitteeRaiseLetterViewModel model = new CommitteeRaiseLetterViewModel();
            model.CommitteeRaiseLetter = _iCommitteeRaiseLetterManager.FindCommitteeRaiseLetterById(CommitteeRaiseLetterId);
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(CommitteeRaiseLetterViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            CommitteeRaiseLetter committeeRaiseLetter = new CommitteeRaiseLetter();
            long CommitteeRaiseLetterId = model.CommitteeRaiseLetter.CommitteeRaiseLetterId;
            committeeRaiseLetter = _iCommitteeRaiseLetterManager.FindCommitteeRaiseLetterById(CommitteeRaiseLetterId);
            int result = _iCommitteeRaiseLetterManager.DeleteCommitteeRaiseLetter(committeeRaiseLetter);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            long CommitteeRaiseLetterId = Convert.ToInt64(id);
            CommitteeRaiseLetterViewModel model = new CommitteeRaiseLetterViewModel();
            model.CommitteeRaiseLetter = _iCommitteeRaiseLetterManager.FindCommitteeRaiseLetterById(CommitteeRaiseLetterId);
            model.CommonCodeTypeValues = CommonCodeTypeValueSelectedList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(CommitteeRaiseLetterViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            CommitteeRaiseLetter committeeRaiseLetter = new CommitteeRaiseLetter();
            committeeRaiseLetter = model.CommitteeRaiseLetter;
            model.Message = _iCommitteeRaiseLetterManager.EditCommitteeRaiseLetter(committeeRaiseLetter);
            model.CommonCodeTypeValues = CommonCodeTypeValueSelectedList();
            return View(model);
        }
        // AFD Letter Index Page

        [HttpGet]
        public ActionResult AfdApproveList(string flowserial, long? proId, string formId, CommitteeRaiseLetterViewModel model)
        {
            model.FileNo = (long) proId;
            model.FlowSerial = flowserial;
            model.FormId = formId;
            model.AFDLetterApproves = _afdLetterApproveManager.GetAll(proId);
            return View(model);
        }
        // AFD Letter Edit And Create Page
        [HttpGet]
        public ActionResult AfdApproveIndex(string fileId, string flowserial, string formId, CommitteeRaiseLetterViewModel model)
        {

            model.ProjectLists = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
        
            model.AFDLetterApprove = _afdLetterApproveManager.GetAFDApprove(fileId) ?? new AFDLetterApprove();
            model.AfdLetter = _afdLetterManager.GetAFDLetterByFile(fileId) ?? new AFDLetter();
            model.FileNo = Convert.ToInt64(fileId);
            model.FlowSerial = flowserial;
            model.FormId = formId;
            return View(model);
        }
        // Afd Letter Save 

        [HttpPost]
        public ActionResult AfdApproveSave(CommitteeRaiseLetterViewModel model)
        {
            try
            {

                string dirPath = @"~/File";
                string dirFullPath = Server.MapPath(dirPath);
                model.AFDLetterApprove.ApproveUrl = ImageUpload.ImageUploadFile(model.ApproveDocument, dirFullPath, dirPath);

                AFDLetterApprove afd = _afdLetterApproveManager.GetAFDApprove(model.FileNo.ToString());
                if (afd != null)
                {
                    _fileBackUpManager.Save(model.FormId, afd.ApproveUrl);
                    afd.AFDLetter = model.AfdLetter.LetterId;
                    afd.IsApprove = model.AFDLetterApprove.IsApprove;
                    afd.ApproveUrl = model.AFDLetterApprove.ApproveUrl ?? afd.ApproveUrl;
                    afd.Reference = model.AFDLetterApprove.Reference;
                    afd.Decition = model.AFDLetterApprove.Decition;
                    afd.Remarks = model.AFDLetterApprove.Remarks;
                    afd.LastUpdateDate = DateTime.Now.Date;
                    afd.LastUpdateId = PortalContext.CurrentUser.UserName;
                    model.AFDLetterApprove = afd;
                    _flowSetupManager.UpdateFormStatusInFlow(model.FileNo, model.FlowSerial, FormInformation.AFDLetterApprove, DateTime.Now.Date);

                }
                else
                {
                    model.AFDLetterApprove.AFDLetter = model.AfdLetter.LetterId;
                    model.AFDLetterApprove.CreateDate = DateTime.Now.Date;
                    model.AFDLetterApprove.CreateId = PortalContext.CurrentUser.UserName;
                    _flowSetupManager.UpdateFormStatusInFlow(model.FileNo, model.FlowSerial, FormInformation.AFDLetterApprove, DateTime.Now.Date);

                }
                int result = _afdLetterApproveManager.Create(model.AFDLetterApprove);

                if (result == 1)
                {
                    model.SuccessMessage = "AFD Letter Approved Successfully!";

                }
                else
                {
                    model.FailedMessage = "AFD Letter Approved Failed!";
                }
            }
            catch (WebException ex)
            {

                model.FailedMessage = ex.Message;
                model.ProjectLists = _demandManager.GetAll().Select(x => new SelectionList()
                {
                    Value = x.DemandId,
                    Text = x.FileNo
                }).ToList();
                return View("AfdApproveIndex", model);  

            }
            model.ProjectLists = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            return View("AfdApproveIndex", model);  
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