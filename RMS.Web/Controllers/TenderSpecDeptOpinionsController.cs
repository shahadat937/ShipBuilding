using System.Drawing;
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
    public class TenderSpecDeptOpinionsController : Controller
    {
        private readonly ITenderSpecDeptOpinionManager _iTenderSpecDeptOpinionManager;
        private readonly IDepartmentManager _iDepartmentManager;
        private readonly ITenderSpecOpinionManager _iTenderSpecOpinionManager;
        private readonly IFlowSetupManager _iFlowSetupManager;
        private readonly IDemandManager _demandManager;
        private readonly IStaffRequirementDeptOpinionManager _staffRequirementDeptOpinionManager;
        private readonly IStaffRequirementOpinionManager _staffRequirementOpinionManager;
        private readonly IContractDeptOpinionManager _contractDeptOpinionManager;
        private readonly IContractOpinionManager _contractOpinionManager;
        public TenderSpecDeptOpinionsController(ITenderSpecDeptOpinionManager iTenderSpecDeptOpinionManager,
            IDepartmentManager iDepartmentManager, ITenderSpecOpinionManager iTenderSpecOpinionManager, IFlowSetupManager iFlowSetupManager
          , IDemandManager demandManager, IStaffRequirementDeptOpinionManager staffRequirementDeptOpinionManager, IContractDeptOpinionManager contractDeptOpinionManager, IStaffRequirementOpinionManager staffRequirementOpinionManager, IContractOpinionManager contractOpinionManager)
        {
            _iTenderSpecDeptOpinionManager = iTenderSpecDeptOpinionManager;
            _iDepartmentManager = iDepartmentManager;
            _iTenderSpecOpinionManager = iTenderSpecOpinionManager;
            _iFlowSetupManager = iFlowSetupManager;
            _demandManager = demandManager;
            _staffRequirementDeptOpinionManager = staffRequirementDeptOpinionManager;
            _contractDeptOpinionManager = contractDeptOpinionManager;
            _staffRequirementOpinionManager = staffRequirementOpinionManager;
            _contractOpinionManager = contractOpinionManager;
        }
        public ActionResult Index(long? fileId, string formId,string flowserial, TenderSpecDeptOpinionViewModel model)
        {
            switch (formId)
            {
                case "10008":
                    model.FormName = "Tender Specification Opinion Information";
                    model.TenderSpecDeptOpinions = _iTenderSpecDeptOpinionManager.GetTenderSpecDeptOpinions(fileId);
                    model.FormId = formId;
                    model.TenderSpecDeptOpinion.FileNo = Convert.ToInt64(fileId);
                    model.FlowSerial = flowserial;
                    return View(model);
                case "10013":
                    model.FormName = "Staff Requirement Opinion Information";
                    model.StaffRequirementDeptOpinions = _staffRequirementDeptOpinionManager.GetstaffRequirementDeptOpinions(fileId);
                    model.FormId = formId;
                    model.TenderSpecDeptOpinion.FileNo = Convert.ToInt64(fileId);
                    model.FlowSerial = flowserial;
                    return View(model);
                case "10015":
                    model.FormName = "Contract Dept Opinion Information";
                    model.ContractDeptOpinions = _contractDeptOpinionManager.GetContractDeptOpinions(fileId);
                    model.FormId = formId;
                    model.TenderSpecDeptOpinion.FileNo = Convert.ToInt64(fileId);
                    model.FlowSerial = flowserial;
                    return View(model);
                default:
                    break;
            }

            return View(model);
        }
        public ActionResult SearchByKey(TenderSpecDeptOpinionViewModel model)
        {
            if (model.SearchKey == null)
            {
                model.Message = "Sorry ,please search by giving data ";
                return View("Index", model);
            }
            List<TenderSpecDeptOpinion> tenderSpecDeptOpinions = _iTenderSpecDeptOpinionManager.GetTenderSpecificationsBySearchKey(model.SearchKey);
            foreach (var tenderSpecDeptOpinion in tenderSpecDeptOpinions)
            {
                tenderSpecDeptOpinion.OpinionGivenDeptName = _iDepartmentManager.GetAllDepartments().First(x => x.DepartmentId == tenderSpecDeptOpinion.OpinionGivenDeptId).Name;
                model.TenderSpecDeptOpinions.Add(tenderSpecDeptOpinion);
            }
            if (!model.TenderSpecDeptOpinions.Any())
            {
                model.Message = "Not exists";
            }
            return View("Index", model);
        }
        [HttpGet]
        public ActionResult Create(string fileId, string flowserial, string formId, TenderSpecDeptOpinionViewModel model)
        {
            switch (formId)
            {
                case "10008":
                    model.Name = "Tender Name :";
                    //model.Url = "Tender URL :";
                    model.FileSelectedItems = GetSelectedFileNo();
                    model.TenderSpecDeptOpinion.FileNo = Convert.ToInt64(fileId);
                    model.FlowSerial = flowserial;
                    model.FormName = "Tender Specification Opinion Information";
                    model.FormId = formId;
                    return View(model);
                case "10013":
                    model.Name = "Staff Requirement Name :";
                    //model.Url = "Staff Requirement URL :";
                    model.FileSelectedItems = GetSelectedFileNo();
                    model.FormName = "Staff Requirement Opinion Information";
                    model.FormId = formId;
                    model.TenderSpecDeptOpinion.FileNo = Convert.ToInt64(fileId);
                    model.FlowSerial = flowserial;
                    return View(model);
                case "10015":
                    model.Name = "Contract Name :";
                    //model.Url = "Contract URL :";
                    model.FileSelectedItems = GetSelectedFileNo();
                    model.TenderSpecDeptOpinion.FileNo = Convert.ToInt64(fileId);
                    model.FlowSerial = flowserial;
                    model.FormName = "Contract Dept Opinion Information";
                    model.FormId = formId;
                    return View(model);
                default:
                    break;
            }
            return View(model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult SaveCreate(TenderSpecDeptOpinionViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Flow Url Status Update 
            //_iFlowSetupManager.UpdateFormStatusInFlow(model.TenderSpecDeptOpinion.FileNo, model.FlowSerial, FormInformation.TenderSpecificationDeptOpinion);
            _iFlowSetupManager.UpdateFormStatusInFlow(model.TenderSpecDeptOpinion.FileNo, model.FlowSerial, model.FormId,DateTime.Now);

            model.Message = _iTenderSpecDeptOpinionManager.SaveTenderSpecDeptOpinionWithOpinionUrl(model.TenderSpecDeptOpinion, model.FormId);
            switch (model.FormId)
            {
                case "10008":
                    model.Name = "Tender Name :";
                    model.FormName = "Tender Specification Opinion Information";
                    break;
                case "10013":
                    model.Name = "Staff Requirement Name :";
                    model.FormName = "Staff Requirement Opinion Information";
                    break;
                case "10015":
                    model.Name = "Contract Name :";
                    model.FormName = "Contract Dept Opinion Information";
                    break;
            }
            model.FileSelectedItems = GetSelectedFileNo();
            return View("Create", model);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            long tenderSpecDeptOpinionId = Convert.ToInt64(id);
            TenderSpecDeptOpinionViewModel model = new TenderSpecDeptOpinionViewModel();
            model.TenderSpecDeptOpinion = _iTenderSpecDeptOpinionManager.GetTenderSpecDeptOpinionById(tenderSpecDeptOpinionId);
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(TenderSpecDeptOpinionViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            TenderSpecDeptOpinion tenderSpecDeptOpinion = new TenderSpecDeptOpinion();
            long tenderSpecDeptOpinionId = Convert.ToInt64(model.TenderSpecDeptOpinion.TenderSpecOpinionGivenId);
            tenderSpecDeptOpinion = _iTenderSpecDeptOpinionManager.GetTenderSpecDeptOpinionById(tenderSpecDeptOpinionId);
            _iTenderSpecDeptOpinionManager.Delete(tenderSpecDeptOpinion);
            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    long tenderSpecDeptOpinionId = Convert.ToInt64(id);
        //    TenderSpecDeptOpinionViewModel model = new TenderSpecDeptOpinionViewModel();
        //    model.TenderSpecDeptOpinion = _iTenderSpecDeptOpinionManager.GetTenderSpecDeptOpinionById(tenderSpecDeptOpinionId);
        //    model.FileSelectedItems = GetSelectedFileNo();
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult Edit(TenderSpecDeptOpinionViewModel model)
        //{
        //    if (model == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TenderSpecDeptOpinion tenderSpecDeptOpinion = new TenderSpecDeptOpinion();
        //    tenderSpecDeptOpinion = model.TenderSpecDeptOpinion;
        //    model.Message = _iTenderSpecDeptOpinionManager.EditTenderSpecDeptOpinionWithOpinionUrl(tenderSpecDeptOpinion);
        //    model.FileSelectedItems = GetSelectedFileNo();
        //    return View(model);
        //}
        public List<SelectionList> GetSelectedFileNo()
        {
            List<SelectionList> SelectedFiles = new List<SelectionList>();
            List<Demand> fileNoList = _demandManager.GetAll();
            fileNoList.ForEach(x => SelectedFiles.Add(new SelectionList() { Text = x.FileNo, Value = x.DemandId }));
            return SelectedFiles;
        }
        [HttpGet]
        public JsonResult GetTenderNamesByFileNo(string tenderSpecFileNo, string formId)
        {
            List<SelectionList> tenderSelectNames = new List<SelectionList>();
            switch (formId)
            {
                case "10008":
                  List<TenderSpecOpinion> tenderListByFileNo = _iTenderSpecOpinionManager.GetTenderListByFileNo(tenderSpecFileNo);
            tenderSelectNames = tenderListByFileNo.Select(x => new SelectionList()
            {
                Text = x.TenderName,
                Value = x.OpinionId
            }).ToList();
                    break;
                case "10013":
                       List<StaffRequirementOpinion> tenderListByFileNoo = _staffRequirementOpinionManager.GetStaffRequireListByFileNo(tenderSpecFileNo);
                       tenderSelectNames = tenderListByFileNoo.Select(x => new SelectionList()
                        {
                            Text = x.TenderName,
                            Value = x.OpinionId
                        }).ToList();
                       break;
                case "10015":
                 List<ContractOpinion> tenderListByFileNooo = _contractOpinionManager.GetContractOpinionListByFileNo(tenderSpecFileNo);
                 tenderSelectNames = tenderListByFileNooo.Select(x => new SelectionList()
                        {
                            Text = x.TenderName,
                            Value = x.OpinionId
                        }).ToList();
                       break;
            }
        
            //tenderListByFileNo.ForEach(x => tenderSelectNames.Add(new SelectListItem() { Text = x.TenderName, Value = x.TenderName }));
            return Json(tenderSelectNames, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepartmentsByTenderName(long tenderName, string fileNo, string formId)
        {
            List<SelectListItem> departmentSelectedList = new List<SelectListItem>();
            long tenderId = Convert.ToInt64(fileNo);
            switch (formId)
            {
                case "10008":
                       string tendername = _iTenderSpecOpinionManager.GetTenderSpecOpinionById(tenderId).TenderName;
            List<TenderSpecDeptOpinion> departmentList = _iTenderSpecDeptOpinionManager.GetDepartmentListByTenderName(tendername, fileNo);
            departmentList.ForEach(x => departmentSelectedList.Add(new SelectListItem() { Text = _iDepartmentManager.GetDeptNameByDeptId(x.OpinionGivenDeptId), Value = x.OpinionGivenDeptId.ToString() }));
                    break;
                case "10013":
                    string tendernamee = _staffRequirementOpinionManager.GetstaffRequirementById(tenderId).TenderName;
                    List<StaffRequirementDeptOpinion> departmentListt = _staffRequirementDeptOpinionManager.GetDepartmentListByTenderName(tendernamee, fileNo);
            departmentListt.ForEach(x => departmentSelectedList.Add(new SelectListItem() { Text = _iDepartmentManager.GetDeptNameByDeptId(x.OpinionGivenDeptId), Value = x.OpinionGivenDeptId.ToString() }));
                    break;
                case "10015":
                        string tendernameee = _contractOpinionManager.GetContractOpinionById(tenderId).TenderName;
                    List<ContractDeptOpinion> departmentListtt = _contractDeptOpinionManager.GetDepartmentListByTenderName(tendernameee, fileNo);
                    departmentListtt.ForEach(x => departmentSelectedList.Add(new SelectListItem() { Text = _iDepartmentManager.GetDeptNameByDeptId(x.OpinionGivenDeptId), Value = x.OpinionGivenDeptId.ToString() }));
                    break;
            }
            return Json(departmentSelectedList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFileName(long? fileNo, string formId, long? depId)
        {
            vwOpinionDeptInfo departmentInfo = new vwOpinionDeptInfo();
           
            switch (formId)
            {
                    // Tender Spec Dept Opinion
                case "10008":
                    var oldData = _iTenderSpecDeptOpinionManager.GetOpinionByProjectAndDept(fileNo, depId);
                    if (oldData != null)
                    {
                        if (oldData.OpinionUrl != null)
                        {
                            Uri uri = new Uri(oldData.OpinionUrl);
                            string filename = System.IO.Path.GetFileName(uri.LocalPath);
                            departmentInfo.FileURL = oldData.OpinionUrl;
                            departmentInfo.FileName = filename;
                        }
         
                        
                        departmentInfo.Remarks = oldData.Remarks;
                    
                    }
                 
                    break;
                // Staff Requirement Dept Opinion
                case "10013":
                    var oldDataa = _staffRequirementDeptOpinionManager.GetOpinionByProjectAndDept(fileNo, depId);
                    if (oldDataa.OpinionUrl != null)
                    {
                            Uri urii = new Uri(oldDataa.OpinionUrl);
                            string filenamee = System.IO.Path.GetFileName(urii.LocalPath);
                            departmentInfo.FileURL = oldDataa.OpinionUrl;
                            departmentInfo.FileName = filenamee;
                    }
                    departmentInfo.Remarks = oldDataa.Remarks;
                    break;
                // Contract Dept Opinion
                case "10015":
                    var oldDataaa = _contractDeptOpinionManager.GetOpinionByProjectAndDept(fileNo, depId);
                    if (oldDataaa.OpinionUrl != null)
                    {
                        Uri uriii = new Uri(oldDataaa.OpinionUrl);
                            string filenameee = System.IO.Path.GetFileName(uriii.LocalPath);
                            departmentInfo.FileURL = oldDataaa.OpinionUrl;
                            departmentInfo.FileName = filenameee;
                    }
                    departmentInfo.Remarks = oldDataaa.Remarks;
         
                    break;
         
            }
            return Json(departmentInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TenderSpecificationDelete()
        {

            return View("Index");
        }
    }
}