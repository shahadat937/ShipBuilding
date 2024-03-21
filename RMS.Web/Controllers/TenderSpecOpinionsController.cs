using System.IO;
using ClosedXML.Excel;
using iTextSharp.text.pdf.qrcode;
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
    public class TenderSpecOpinionsController : Controller
    {
        private readonly ITenderSpecOpinionManager _iTenderSpecOpinionManager;
        public readonly ITenderSpecDeptOpinionManager _iTenderSpecDeptOpinionManager;
        private readonly IDepartmentManager _iDepartmentManager;
        private readonly IFlowSetupManager _flowSetupManager;
        private readonly IDemandManager _demandManager;
        private readonly IStaffRequirementOpinionManager _staffRequirementOpinionManager;
        private readonly IContractOpinionManager _contractOpinionManager;
        private readonly IStaffRequirementDeptOpinionManager _staffRequirementDeptOpinionManager;
        private readonly IContractDeptOpinionManager _contractDeptOpinionManager;
        public TenderSpecOpinionsController(ITenderSpecOpinionManager iTenderSpecOpinionManager,
            IDepartmentManager iDepartmentManager, ITenderSpecDeptOpinionManager iTenderSpecDeptOpinionManager, IFlowSetupManager flowSetupManager, IDemandManager demandManager, IStaffRequirementOpinionManager staffRequirementOpinionManager, IContractOpinionManager contractOpinionManager, IStaffRequirementDeptOpinionManager staffRequirementDeptOpinionManager, IContractDeptOpinionManager contractDeptOpinionManager)
        {
            _iTenderSpecOpinionManager = iTenderSpecOpinionManager;
            _iDepartmentManager = iDepartmentManager;
            _iTenderSpecDeptOpinionManager = iTenderSpecDeptOpinionManager;
            _flowSetupManager = flowSetupManager;
            _demandManager = demandManager;
            _staffRequirementOpinionManager = staffRequirementOpinionManager;
            _contractOpinionManager = contractOpinionManager;
            _staffRequirementDeptOpinionManager = staffRequirementDeptOpinionManager;
            _contractDeptOpinionManager = contractDeptOpinionManager;
        }
        public ActionResult Index(string formId, long? proId, string flowserial, TenderSpecOpinionViewModel model)
        {
            switch (formId)
            {
                case "10004":
                    model.Name = "Tender Name :";
                    model.Url = "Tender URL :";
                    model.FormName = "Tender Specification Opinion ";
                    model.TenderSpecOpinions = _iTenderSpecOpinionManager.GetTenderDeptInfoByProject(proId);
                    break;

                case "10012":
                    List<TenderSpecOpinion> st = new List<TenderSpecOpinion>();
                    var ss = _staffRequirementOpinionManager.GetStaffRequireListByFileNo(proId.ToString());
                    foreach (var staffRequirementOpinion in ss)
                    {
                        TenderSpecOpinion top = new TenderSpecOpinion();
                        top.Demand = new Demand();
                        top.OpinionId = staffRequirementOpinion.OpinionId;
                        top.FileNo = staffRequirementOpinion.OpinionId;
                        top.Demand.FileNo = staffRequirementOpinion.Demand.FileNo;
                        top.TenderName = staffRequirementOpinion.TenderName;
                        top.TenderUrl = staffRequirementOpinion.TenderUrl;
                        top.IssueDate = staffRequirementOpinion.IssueDate;
                        top.ReceivedDate = staffRequirementOpinion.ReceivedDate;
                        top.Remarks = staffRequirementOpinion.Remarks;
                        top.CreatedBy = staffRequirementOpinion.CreatedBy;
                        top.CreatedDate = staffRequirementOpinion.CreatedDate;
                        top.UpdatedBy = staffRequirementOpinion.UpdatedBy;
                        top.UpdatedDate = staffRequirementOpinion.UpdatedDate;
                        st.Add(top);
                    }
                    model.TenderSpecOpinions = st;
                    model.Name = "Staff Requirement Name :";
                    model.Url = "Staff Requirement URL :";
                    model.FormName = "Staff Requirement Opinion";
                    break;
                case "10014":

                    List<TenderSpecOpinion> tenderSpecOpinions = new List<TenderSpecOpinion>();
                    List<ContractOpinion> contractOpinion = _contractOpinionManager.GetContractOpinionListByFileNo(proId.ToString());
                    foreach (var item in contractOpinion)
                    {
                        TenderSpecOpinion top = new TenderSpecOpinion();
                        top.Demand = new Demand();
                        top.OpinionId = item.OpinionId;
                        top.FileNo = item.OpinionId;
                        top.Demand.FileNo = item.Demand.FileNo;
                        top.TenderName = item.TenderName;
                        top.TenderUrl = item.TenderUrl;
                        top.IssueDate = item.IssueDate;
                        top.ReceivedDate = item.ReceivedDate;
                        top.Remarks = item.Remarks;
                        top.CreatedBy = item.CreatedBy;
                        top.CreatedDate = item.CreatedDate;
                        top.UpdatedBy = item.UpdatedBy;
                        top.UpdatedDate = item.UpdatedDate;
                        tenderSpecOpinions.Add(top);
                    }
                    model.TenderSpecOpinions = tenderSpecOpinions;
                    model.Name = "Contract Name :";
                    model.Url = "Contract URL :";
                    model.FormName = "Contract Opinion";
                    break;
                    //default:

            }
            model.FormId = formId;
            model.TenderSpecOpinion.FileNo = (long)proId;
            model.FileSerial = flowserial;
            return View(model);
        }
        public ActionResult Index1()
        {
            TenderSpecOpinionViewModel model = new TenderSpecOpinionViewModel();
            model.TenderSpecOpinions = _iTenderSpecOpinionManager.GetAllTenderSpecifications();
            return View(model);
        }
        //public ActionResult SearchByKey(TenderSpecOpinionViewModel model)
        //{
        //    if (model.SearchKey == null)
        //    {
        //        model.Message = "Sorry ,please search by giving data ";
        //        return View("Index", model);
        //    }
        //    string searchKey = model.SearchKey;
        //    searchKey = searchKey.ToLower();
        //    model.TenderSpecOpinions = _iTenderSpecOpinionManager.GetTenderSpecificationsBySearchKey(searchKey);
        //    if (!model.TenderSpecOpinions.Any())
        //    {
        //        model.Message = "Not exists";
        //    }
        //    return View("Index", model);
        //}
        public ActionResult SearchByKey(TenderSpecOpinionViewModel model)
        {
            List<TenderSpecOpinion> tenderSpecOps = _iTenderSpecOpinionManager.GetAllTenderSpecifications();
            if (model.SearchKey == null)
            {
                model.TenderSpecOpinions = tenderSpecOps;
                model.Message = "Please enter a keyword to search ";
            }
            else
            {
                string searchKey = model.SearchKey.ToLower();
                List<TenderSpecOpinion> tenderSpecOpinions = _iTenderSpecOpinionManager.GetAllTenderSpecificationBySearchKey(searchKey);
                if (tenderSpecOpinions.Count > 0)
                {
                    model.TenderSpecOpinions = tenderSpecOpinions;
                }
                else
                {
                    model.TenderSpecOpinions = tenderSpecOps;
                    model.Message = "Data is not found with this " + "'" + searchKey + "'";
                }
            }

            return View("Index1", model);
        }
        [HttpGet]
        public ActionResult Create(string fileId, string flowserial, string formId, TenderSpecOpinionViewModel model)
        {
            long FileId = Convert.ToInt64(fileId);
            model.ProjectSelectionLists = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            switch (formId)
            {
                case "10004":
               
                    model.TenderSpecOpinion = _iTenderSpecOpinionManager.GetTenderSpecOpinionById(FileId) ?? new TenderSpecOpinion();

                    if (model.TenderSpecOpinion != null)
                    {
                        List<TenderSpecDeptOpinion> tenderSpecDeptOpinions =
                            _iTenderSpecDeptOpinionManager.GetTenderSpecDeptOpinionByFileNo(model.TenderSpecOpinion.FileNo);

                        List<SelectListItem> departmentList = DepartmentSelectedItems();
                        foreach (var dept in departmentList)
                        {
                            foreach (var item in tenderSpecDeptOpinions)
                            {
                                if (dept.Value == Convert.ToString(item.OpinionGivenDeptId))
                                {
                                    dept.Selected = true;
                                }
                            }
                            model.DepartmentList.Add(dept);
                        }
                    }
                    //​        model.FormName = "Tender Specification Opinion";
                    //model.Name = "Tender Name :";
                    //​        model.Url = "Tender URL :";
                    break;

                case "10012":

                    StaffRequirementOpinion ss = _staffRequirementOpinionManager.GetstaffRequirementById(FileId) ?? new StaffRequirementOpinion();

                    model.TenderSpecOpinion.OpinionId = ss.OpinionId;
                    model.TenderSpecOpinion.FileNo = ss.OpinionId;
                    model.TenderSpecOpinion.TenderName = ss.TenderName;
                    model.TenderSpecOpinion.TenderUrl = ss.TenderUrl;
                    model.TenderSpecOpinion.IssueDate = ss.IssueDate;
                    model.TenderSpecOpinion.ReceivedDate = ss.ReceivedDate;
                    model.TenderSpecOpinion.Remarks = ss.Remarks;
                    model.TenderSpecOpinion.CreatedBy = ss.CreatedBy;
                    model.TenderSpecOpinion.CreatedDate = ss.CreatedDate;
                    model.TenderSpecOpinion.UpdatedBy = ss.UpdatedBy;
                    model.TenderSpecOpinion.UpdatedDate = ss.UpdatedDate;
                    if (model.TenderSpecOpinion != null)
                    {
                        List<StaffRequirementDeptOpinion> tenderSpecDeptOpinions =
                            _staffRequirementDeptOpinionManager.GetTenderSpecDeptOpinionByFileNo(FileId);

                        List<SelectListItem> departmentList = DepartmentSelectedItems();
                        foreach (var dept in departmentList)
                        {
                            foreach (var item in tenderSpecDeptOpinions)
                            {
                                if (dept.Value == Convert.ToString(item.OpinionGivenDeptId))
                                {
                                    dept.Selected = true;
                                }
                            }
                            model.DepartmentList.Add(dept);
                        }
                    }
                    model.FormName = "Staff Requirement Opinion";
                    model.Name = "Staff Requirement Name :";
                    model.Url = "Staff Requirement URL :";
                    break;
                case "10014":

                    ContractOpinion sas = _contractOpinionManager.GetContractOpinionById(FileId) ?? new ContractOpinion();

                    model.TenderSpecOpinion.OpinionId = sas.OpinionId;
                    model.TenderSpecOpinion.FileNo = sas.OpinionId;
                    model.TenderSpecOpinion.TenderName = sas.TenderName;
                    model.TenderSpecOpinion.TenderUrl = sas.TenderUrl;
                    model.TenderSpecOpinion.IssueDate = sas.IssueDate;
                    model.TenderSpecOpinion.ReceivedDate = sas.ReceivedDate;
                    model.TenderSpecOpinion.Remarks = sas.Remarks;
                    model.TenderSpecOpinion.CreatedBy = sas.CreatedBy;
                    model.TenderSpecOpinion.CreatedDate = sas.CreatedDate;
                    model.TenderSpecOpinion.UpdatedBy = sas.UpdatedBy;
                    model.TenderSpecOpinion.UpdatedDate = sas.UpdatedDate;
                    if (model.TenderSpecOpinion != null)
                    {
                        List<ContractDeptOpinion> tenderSpecDeptOpinions =
                            _contractDeptOpinionManager.GetTenderSpecDeptOpinionByFileNo(FileId);

                        List<SelectListItem> departmentList = DepartmentSelectedItems();
                        foreach (var dept in departmentList)
                        {
                            foreach (var item in tenderSpecDeptOpinions)
                            {
                                if (dept.Value == Convert.ToString(item.OpinionGivenDeptId))
                                {
                                    dept.Selected = true;
                                }
                            }
                            model.DepartmentList.Add(dept);
                        }
                    }
                    model.FormName = "Contract Opinion";
                    model.Name = "Contract Name :";
                    model.Url = "Contract URL :";
                    break;
                //default:
                default:
                    break;

            }

            model.FormId = formId;
            model.TenderSpecOpinion.FileNo = FileId;
            model.FileSerial = flowserial;

            return View(model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult SaveCreate(TenderSpecOpinionViewModel model)
        {
            model.ProjectSelectionLists = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            switch (model.FormId)
            {
                case "10004":
                    model.Name = "Tender Name :";
                    model.Url = "Tender URL :";
                    model.FormName = "Tender Specification Opinion";
                    TenderSpecOpinion olddata = _iTenderSpecOpinionManager.GetExistDataByIdentity(model.TenderSpecOpinion.OpinionId);
                    if (olddata != null)
                    {
                        if (model.ImageUpload != null)
                        {

                            string dirPath = @"~/File";
                            string dirFullPath = Server.MapPath(dirPath);
                            model.TenderSpecOpinion.TenderUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                        }
                        olddata.TenderName = model.TenderSpecOpinion.TenderName;
                        olddata.TenderUrl = model.TenderSpecOpinion.TenderUrl ?? olddata.TenderUrl;
                        olddata.IssueDate = model.TenderSpecOpinion.IssueDate;
                        olddata.Remarks = model.TenderSpecOpinion.Remarks;
                        olddata.UpdatedBy = PortalContext.CurrentUser.UserName;
                        olddata.UpdatedDate = DateTime.Now;
                        model.Message = _iTenderSpecOpinionManager.SaveTenderSpecOpinion(olddata);
                        TenderSpecDeptOpinion tenderSpecDeptOpinion = new TenderSpecDeptOpinion();
                        _iTenderSpecDeptOpinionManager.DeleteAllDept(model.TenderSpecOpinion.FileNo);
                        foreach (var modelObj in model.DepartmentList)
                        {
                            if (modelObj.Selected == true)
                            {
                                tenderSpecDeptOpinion.FileNo = model.TenderSpecOpinion.FileNo;
                                tenderSpecDeptOpinion.TenderName = model.TenderSpecOpinion.TenderName;
                                tenderSpecDeptOpinion.IssueDate = model.TenderSpecOpinion.IssueDate;
                                tenderSpecDeptOpinion.OpinionGivenDeptId = Convert.ToInt64(modelObj.Value);
                                tenderSpecDeptOpinion.UpdatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.UpdatedDate = DateTime.Now;
                                _iTenderSpecDeptOpinionManager.SaveTenderSpecDeptOpinion(tenderSpecDeptOpinion);
                            }

                        }
                        model.DepartmentList = DepartmentSelectedItems();
                        return View(model);
                    }
                    else
                    {
                        int KK = 0;
                        TenderSpecDeptOpinion tenderSpecDeptOpinion = new TenderSpecDeptOpinion();
                        if (model.ImageUpload != null)
                        {

                            string dirPath = @"~/File";
                            string dirFullPath = Server.MapPath(dirPath);
                            model.TenderSpecOpinion.TenderUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                        }
                        model.Message = _iTenderSpecOpinionManager.SaveTenderSpecOpinion(model.TenderSpecOpinion);
                        foreach (var modelObj in model.DepartmentList)
                        {
                            if (modelObj.Selected == true)
                            {

                                // Flow Url Status Update 
                                if (model.TenderSpecOpinion.FileNo != null && model.FileSerial != null && KK == 0)
                                {
                                    _flowSetupManager.UpdateFormStatusInFlow(model.TenderSpecOpinion.FileNo, model.FileSerial, FormInformation.TenderSpecificationOpinion, DateTime.Now);
                                    ++KK;
                                }

                                tenderSpecDeptOpinion.FileNo = model.TenderSpecOpinion.FileNo;
                                tenderSpecDeptOpinion.TenderName = model.TenderSpecOpinion.TenderName;
                                tenderSpecDeptOpinion.IssueDate = model.TenderSpecOpinion.IssueDate;
                                tenderSpecDeptOpinion.OpinionGivenDeptId = Convert.ToInt64(modelObj.Value);
                                tenderSpecDeptOpinion.CreatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.CreatedDate = DateTime.Now;
                                tenderSpecDeptOpinion.UpdatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.UpdatedDate = DateTime.Now;
                                _iTenderSpecDeptOpinionManager.SaveTenderSpecDeptOpinion(tenderSpecDeptOpinion);
                            }

                        }
                        return View("Create", model);
                    }
                // Staff Requrement Opinion 
                case "10012":
                    model.Name = "Staff Requirement Name :";
                    model.Url = "Staff Requirement URL :";
                    model.FormName = "Staff Requirement Opinion";

                    StaffRequirementOpinion olddataa = _staffRequirementOpinionManager.GetExistDataByIdentity(model.TenderSpecOpinion.OpinionId);
                    if (olddataa != null)
                    {
                        if (model.ImageUpload != null)
                        {

                            string dirPath = @"~/File";
                            string dirFullPath = Server.MapPath(dirPath);
                            model.TenderSpecOpinion.TenderUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                        }
                        olddataa.TenderName = model.TenderSpecOpinion.TenderName;
                        olddataa.TenderUrl = model.TenderSpecOpinion.TenderUrl ?? olddataa.TenderUrl;
                        olddataa.IssueDate = model.TenderSpecOpinion.IssueDate;
                        olddataa.Remarks = model.TenderSpecOpinion.Remarks;
                        olddataa.UpdatedBy = PortalContext.CurrentUser.UserName;
                        olddataa.UpdatedDate = DateTime.Now;
                        model.Message = _staffRequirementOpinionManager.SaveTenderSpecOpinion(olddataa);
                        StaffRequirementDeptOpinion tenderSpecDeptOpinion = new StaffRequirementDeptOpinion();
                        _staffRequirementDeptOpinionManager.DeleteAllDept(model.TenderSpecOpinion.FileNo);
                        foreach (var modelObj in model.DepartmentList)
                        {
                            if (modelObj.Selected == true)
                            {
                                tenderSpecDeptOpinion.FileNo = model.TenderSpecOpinion.FileNo;
                                tenderSpecDeptOpinion.TenderName = model.TenderSpecOpinion.TenderName;
                                tenderSpecDeptOpinion.IssueDate = model.TenderSpecOpinion.IssueDate;
                                tenderSpecDeptOpinion.OpinionGivenDeptId = Convert.ToInt64(modelObj.Value);
                                tenderSpecDeptOpinion.UpdatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.UpdatedDate = DateTime.Now;
                                _staffRequirementDeptOpinionManager.SaveStaffRequirementDeptOpinion(tenderSpecDeptOpinion);
                            }

                        }
                        model.DepartmentList = DepartmentSelectedItems();
                        return View(model);
                    }
                    else
                    {
                        int KK = 0;
                        StaffRequirementOpinion staffRequirementOpinion = new StaffRequirementOpinion();

                        if (model.ImageUpload != null)
                        {

                            string dirPath = @"~/File";
                            string dirFullPath = Server.MapPath(dirPath);
                            model.TenderSpecOpinion.TenderUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                        }

                        staffRequirementOpinion.OpinionId = model.TenderSpecOpinion.OpinionId;
                        staffRequirementOpinion.FileNo = model.TenderSpecOpinion.FileNo;
                        staffRequirementOpinion.TenderName = model.TenderSpecOpinion.TenderName;
                        staffRequirementOpinion.TenderUrl = model.TenderSpecOpinion.TenderUrl;
                        staffRequirementOpinion.IssueDate = model.TenderSpecOpinion.IssueDate;
                        staffRequirementOpinion.ReceivedDate = model.TenderSpecOpinion.ReceivedDate;
                        staffRequirementOpinion.Remarks = model.TenderSpecOpinion.Remarks;
                        staffRequirementOpinion.CreatedBy = model.TenderSpecOpinion.CreatedBy;
                        staffRequirementOpinion.CreatedDate = model.TenderSpecOpinion.CreatedDate;
                        staffRequirementOpinion.UpdatedBy = model.TenderSpecOpinion.UpdatedBy;
                        staffRequirementOpinion.UpdatedDate = model.TenderSpecOpinion.UpdatedDate;
                        model.Message = _staffRequirementOpinionManager.SaveTenderSpecOpinion(staffRequirementOpinion);
                        StaffRequirementDeptOpinion tenderSpecDeptOpinion = new StaffRequirementDeptOpinion();
                        foreach (var modelObj in model.DepartmentList)
                        {
                            if (modelObj.Selected == true)
                            {

                                // Flow Url Status Update 
                                if (model.TenderSpecOpinion.FileNo != null && model.FileSerial != null && KK == 0)
                                {
                                    _flowSetupManager.UpdateFormStatusInFlow(model.TenderSpecOpinion.FileNo, model.FileSerial, FormInformation.StaffRequirementOpinion, DateTime.Now);
                                    ++KK;
                                }

                                tenderSpecDeptOpinion.FileNo = model.TenderSpecOpinion.FileNo;
                                tenderSpecDeptOpinion.TenderName = model.TenderSpecOpinion.TenderName;
                                tenderSpecDeptOpinion.IssueDate = model.TenderSpecOpinion.IssueDate;
                                tenderSpecDeptOpinion.OpinionGivenDeptId = Convert.ToInt64(modelObj.Value);
                                tenderSpecDeptOpinion.CreatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.CreatedDate = DateTime.Now;
                                tenderSpecDeptOpinion.UpdatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.UpdatedDate = DateTime.Now;
                                _staffRequirementDeptOpinionManager.SaveStaffRequirementDeptOpinion(tenderSpecDeptOpinion);
                            }

                        }

                        return View("Create", model);
                    }
                // Contract Opinion Save
                case "10014":
                    model.Name = "Contract Name :";
                    model.Url = "Contract URL :";
                    model.FormName = "Contract Opinion";
                    ContractOpinion olddataaa = _contractOpinionManager.GetExistDataByIdentity(model.TenderSpecOpinion.OpinionId);
                    if (olddataaa != null)
                    {
                        if (model.ImageUpload != null)
                        {

                            string dirPath = @"~/File";
                            string dirFullPath = Server.MapPath(dirPath);
                            model.TenderSpecOpinion.TenderUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                        }
                        olddataaa.TenderName = model.TenderSpecOpinion.TenderName;
                        olddataaa.TenderUrl = model.TenderSpecOpinion.TenderUrl ?? olddataaa.TenderUrl;
                        olddataaa.IssueDate = model.TenderSpecOpinion.IssueDate;
                        olddataaa.Remarks = model.TenderSpecOpinion.Remarks;
                        olddataaa.UpdatedBy = PortalContext.CurrentUser.UserName;
                        olddataaa.UpdatedDate = DateTime.Now;
                        model.Message = _contractOpinionManager.SaveContractOpinion(olddataaa);
                        ContractDeptOpinion tenderSpecDeptOpinion = new ContractDeptOpinion();
                        _contractDeptOpinionManager.DeleteAllDept(model.TenderSpecOpinion.FileNo);
                        foreach (var modelObj in model.DepartmentList)
                        {
                            if (modelObj.Selected == true)
                            {
                                tenderSpecDeptOpinion.FileNo = model.TenderSpecOpinion.FileNo;
                                tenderSpecDeptOpinion.TenderName = model.TenderSpecOpinion.TenderName;
                                tenderSpecDeptOpinion.IssueDate = model.TenderSpecOpinion.IssueDate;
                                tenderSpecDeptOpinion.OpinionGivenDeptId = Convert.ToInt64(modelObj.Value);
                                tenderSpecDeptOpinion.UpdatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.UpdatedDate = DateTime.Now;
                                _contractDeptOpinionManager.SaveContractDeptOpinion(tenderSpecDeptOpinion);
                            }

                        }
                        model.DepartmentList = DepartmentSelectedItems();
                        return View(model);
                    }
                    else
                    {
                        int KK = 0;
                        ContractOpinion contractField = new ContractOpinion();
                        ContractDeptOpinion tenderSpecDeptOpinion = new ContractDeptOpinion();
                        if (model.ImageUpload != null)
                        {

                            string dirPath = @"~/File";
                            string dirFullPath = Server.MapPath(dirPath);
                            model.TenderSpecOpinion.TenderUrl = ImageUpload.ImageUploadFile(model.ImageUpload, dirFullPath, dirPath);
                        }
                        contractField.OpinionId = model.TenderSpecOpinion.OpinionId;
                        contractField.FileNo = model.TenderSpecOpinion.FileNo;
                        contractField.TenderName = model.TenderSpecOpinion.TenderName;
                        contractField.TenderUrl = model.TenderSpecOpinion.TenderUrl;
                        contractField.IssueDate = model.TenderSpecOpinion.IssueDate;
                        contractField.ReceivedDate = model.TenderSpecOpinion.ReceivedDate;
                        contractField.Remarks = model.TenderSpecOpinion.Remarks;
                        contractField.CreatedBy = model.TenderSpecOpinion.CreatedBy;
                        contractField.CreatedDate = model.TenderSpecOpinion.CreatedDate;
                        contractField.UpdatedBy = model.TenderSpecOpinion.UpdatedBy;
                        contractField.UpdatedDate = model.TenderSpecOpinion.UpdatedDate;
                        model.Message = _contractOpinionManager.SaveTenderSpecOpinion(contractField);
                        foreach (var modelObj in model.DepartmentList)
                        {
                            if (modelObj.Selected == true)
                            {

                                // Flow Url Status Update 
                                if (model.TenderSpecOpinion.FileNo != null && model.FileSerial != null && KK == 0)
                                {
                                    _flowSetupManager.UpdateFormStatusInFlow(model.TenderSpecOpinion.FileNo, model.FileSerial, FormInformation.ContractOpinion, DateTime.Now);
                                    ++KK;
                                }

                                tenderSpecDeptOpinion.FileNo = model.TenderSpecOpinion.FileNo;
                                tenderSpecDeptOpinion.TenderName = model.TenderSpecOpinion.TenderName;
                                tenderSpecDeptOpinion.IssueDate = model.TenderSpecOpinion.IssueDate;
                                tenderSpecDeptOpinion.OpinionGivenDeptId = Convert.ToInt64(modelObj.Value);
                                tenderSpecDeptOpinion.CreatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.CreatedDate = DateTime.Now;
                                tenderSpecDeptOpinion.UpdatedBy = PortalContext.CurrentUser.UserName;
                                tenderSpecDeptOpinion.UpdatedDate = DateTime.Now;
                                _contractDeptOpinionManager.SaveContractDeptOpinion(tenderSpecDeptOpinion);
                            }

                        }
                        return View("Create", model);
                    }

                    //default:

            }

            return View("Create", model);

        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            TenderSpecOpinion model = _iTenderSpecOpinionManager.GetTenderSpecOpinionById(Convert.ToInt64(id));
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TenderSpecOpinionViewModel viewModel = new TenderSpecOpinionViewModel();
            viewModel.TenderSpecOpinion = model;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Delete(TenderSpecOpinionViewModel model)
        {
            if (ModelState.IsValid)
            {
                TenderSpecOpinion tenderSpecOpinion = new TenderSpecOpinion();
                tenderSpecOpinion = _iTenderSpecOpinionManager.GetTenderSpecOpinionById(Convert.ToInt64(model.TenderSpecOpinion.OpinionId));
                if (tenderSpecOpinion != null)
                {
                    _iTenderSpecOpinionManager.Delete(tenderSpecOpinion);
                    return RedirectToAction("Index");
                }
                else
                {
                    model.Message = "Data is not found.";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id, TenderSpecOpinionViewModel model)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            TenderSpecOpinion tenderSpecOpinion = _iTenderSpecOpinionManager.GetTenderSpecOpinionById(Convert.ToInt64(id));
            if (tenderSpecOpinion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.TenderSpecOpinion = tenderSpecOpinion;
            List<TenderSpecDeptOpinion> tenderSpecDeptOpinions = _iTenderSpecDeptOpinionManager.GetTenderSpecDeptOpinionByFileNo(tenderSpecOpinion.FileNo);

            List<SelectListItem> departmentList = DepartmentSelectedItems();
            foreach (var dept in departmentList)
            {
                foreach (var item in tenderSpecDeptOpinions)
                {
                    if (dept.Value == Convert.ToString(item.OpinionGivenDeptId))
                    {
                        dept.Selected = true;
                    }
                }
                model.DepartmentList.Add(dept);
            }
            return View(model);
        }

        public List<SelectListItem> DepartmentSelectedItems()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Department> departmentList = _iDepartmentManager.GetAllDepartments();
            departmentList.ForEach(x => items.Add(new SelectListItem() { Text = x.Name, Value = x.DepartmentId.ToString() }));
            return items;
        }
    }
}