using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Web.Models.ViewModel;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Controllers
{
    public class CommiteeController : BaseController
    {
        private readonly ICommonCodeManager _commonCodeManager;
        private readonly ICommitteNameListManager _committeNameListManager;
        private readonly ICommiteeManager _commiteeManager1;
        private readonly IMemberManager _memberManager;
        private readonly IDemandManager _demandManager;
        private readonly IFlowSetupManager _flowSetupManager;

        public CommiteeController(ICommiteeManager comManager,ICommitteNameListManager committeNameListManager, ICommonCodeManager commonCodeManager, IMemberManager memberManager, IDemandManager demandManager, IFlowSetupManager flowSetupManager)
        {
            _commiteeManager1 = comManager;
            _committeNameListManager = committeNameListManager;
            _commonCodeManager = commonCodeManager;
            _memberManager = memberManager;
            _demandManager = demandManager;
            _flowSetupManager = flowSetupManager;
        }
        [HttpGet]
        public ActionResult Index(string fileId, string flowserial, long? committeId, int? result, CommiteeViewModel model)
        {
            model.CommitteName = _committeNameListManager.GetAll();
            //model.Departments = _iDepartmentManager.GetAllDepartments();
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.CommitteDesignation =
                _commonCodeManager.GetCommonCodeByType("CommitteDesignation").Select(x => new SelectionList()
                {
                    Value = x.CommonCodeID,
                    Text = x.TypeValue
                }).ToList();
            if (fileId != null || flowserial != null)
            {
                model.CommitteInfo.FileNo =Convert.ToInt64(fileId);
                model.FlowSerial = flowserial;
                var committeinfo = _commiteeManager1.GetCommitteInfobyFileId(fileId) ?? new CommitteInfo();
                if (committeinfo!= null && committeId == null) committeId = committeinfo.CommitteName;
            }
            if (committeId > 0)
            {
                model.AllCommitteListed = _commiteeManager1.GetAllCommitteBuId(committeId);
                model.CommitteInfo.CommitteName = (long) committeId;
                if (result == 1)
                {
                    model.SuccessMessage = "Committe has been created successfully!";

                }
        
            }
            return View(model);
        }

        public ActionResult SearchByKey(CommiteeViewModel model)
        {
            //string searchKey = model.Commitee.Name;
            //if (searchKey == null)
            //{
            //    model.Commitees = _commiteeManager1.GetCommitee();
            //}
            //else
            //{
            //    searchKey = searchKey.ToLower();
            //    model.Commitees =
            //        _commiteeManager1.GetCommitee().Where(x => x.Name.ToLower().Contains(searchKey)).ToList();
            //    if (!model.Commitees.Any())
            //    {
            //        //model.SuccessMessage = 0;
            //        model.FailedMessage = "Data is not found.";
            //    }
            //}
            return View( model);
        }

        [HttpGet]
        public ActionResult Create(string CommiteeId, CommiteeViewModel model)
        {
            //model.StaffReqierment = _iStaffRequirementManager.GetStaffRequirement();
            //model.Department = _iDepartmentManager.GetAllDepartments();
            //if (CommiteeId != null)
            //{
            //    model.Commitee = _commiteeManager1.GetCommiteeById(Convert.ToInt64(CommiteeId));
            //}
            return View(model);
        }

        public JsonResult MemberNameGet(string membername)
        {
            var list = _memberManager.GetAll().Where(x => x.MemberName.ToLower().StartsWith(membername.ToLower())).ToList();

            List<vwMemberNameInfo> fileInfoes = new List<vwMemberNameInfo>();
            foreach (var item in list)
            {
                vwMemberNameInfo fileInfo = new vwMemberNameInfo();
                fileInfo.MemberId = item.MemberId;
                fileInfo.MemberName = item.MemberName;
                fileInfoes.Add(fileInfo);
            }
            return Json(fileInfoes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(CommiteeViewModel model)
        {
            var flowse = model.FlowSerial;
            if (model != null)
            {
                CommitteInfo fs = new CommitteInfo();
                //var fn = _iFlowListManager.GetFlowListByFlowId(model.FlowSetup.FlowId.Value).FlowName;
                fs = _commiteeManager1.GetCommitteInfo(model.CommitteInfo.CommiteeId);
                if (fs != null)
                {
                    fs.CommitteName = model.CommitteInfo.CommitteName;
                    fs.MemberId = model.CommitteInfo.MemberId;
                    //fs.DepartmentId = model.CommitteInfo.DepartmentId;
                    fs.WorkPlace = model.CommitteInfo.WorkPlace;
                    //fs.PhoneNo = model.CommitteInfo.PhoneNo;
                  
                    fs.CommitteDesignation = model.CommitteInfo.CommitteDesignation;
                    model.CommitteInfo = fs;

                }
                else
                {
                    // Flow Url Status Update 
                    _flowSetupManager.UpdateFormStatusInFlow(model.CommitteInfo.FileNo, model.FlowSerial, FormInformation.Committe,DateTime.Now);
             
                    model.CommitteInfo.UserId = PortalContext.CurrentUser.UserName;
                    model.CommitteInfo.UpdatedBy = PortalContext.CurrentUser.UserName;
                    model.CommitteInfo.EntryDate = DateTime.Now;
                    model.CommitteInfo.LastUpdate = DateTime.Now;
                }

                model.Result = _commiteeManager1.CreateCommitte(model.CommitteInfo);

                //if (result == 1)
                //{
                //    model.SuccessMessage = "Committe has been created successfully!";

                //}
                //else
                //{
                //    model.FailedMessage = "Committe creation failed!";
                //}

            }

            return RedirectToAction("Index", new { fileId = model.CommitteInfo.FileNo, flowserial = flowse, committeId = model.CommitteInfo.CommitteName, result = model.Result });
        }

        [HttpPost]
        public ActionResult CommitteCreate(CommiteeViewModel model)
        {
            if (model != null)
            {
                CommitteNameList fs = new CommitteNameList();
                fs = _committeNameListManager.GetCommitteName(model.CommitteNameList.CommitteName);
                int result = 0;
                if (fs != null)
                {
                    model.FailedMessage = "Committe Name Already Exist!";
                }
                else
                {
                    model.CommitteNameList.CreateId = PortalContext.CurrentUser.UserName;
                    model.CommitteNameList.UpdateId = PortalContext.CurrentUser.UserName;
                    model.CommitteNameList.CreateDate = DateTime.Now;
                    model.CommitteNameList.UpdateDate = DateTime.Now;
                    result = _committeNameListManager.CreateCommitteName(model.CommitteNameList);
                }
                if (result == 1)
                {
                    model.SuccessMessage = "Committe Name has been created successfully!";
                }
                else
                {
                    model.FailedMessage = "Committe Name creation failed!";
                }

            }
            return RedirectToAction("Index",new { fileId = model.CommitteInfo.FileNo, flowserial = model.FlowSerial});
        }

        public ActionResult Delete(long? id,string fileIdentity,long? committeIdentity)
        {
            var del = _commiteeManager1.DeleteCommitteInfo(id);
            return RedirectToAction("Index", new { fileId = fileIdentity, committeId = committeIdentity });

        }

    }
}