using System.Globalization;
using System.Management.Automation;
using iTextSharp.text.pdf.qrcode;
using RMS.BLL.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using RMS.Web.Models.ViewModel;
using RMS.Utility;

namespace RMS.Web.Controllers
{
    public class FlowSetupController : Controller
    {
      
    
        private readonly IDepartmentManager _iDepartmentManager;
        private readonly IFlowSetupManager _iFlowSetupManager;
        private readonly IFlowListManager _iFlowListManager;
        private readonly IFormInfoManager _iforminfoManager;
        private readonly IDemandManager _demandManager;
        public FlowSetupController( IDepartmentManager iDepartmentManager, IFlowSetupManager iFlowSetupManager, IFlowListManager iFlowListManager, IFormInfoManager iforminfoManager, IDemandManager demandManager)
        {
          
            _iDepartmentManager = iDepartmentManager;
            _iFlowSetupManager = iFlowSetupManager;
            _iFlowListManager = iFlowListManager;
            _iforminfoManager = iforminfoManager;
            _demandManager = demandManager;
        }
        public ActionResult Index(string FlowId, FlowSetupViewModel model)
        {
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.Key = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            model.FlowSetupEditDictionary.Add(model.Key, new SelectionList());
            model.FormInfos = _iforminfoManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.FormId,
                Text = x.FormName
            }).ToList();
            model.Department = _iDepartmentManager.GetAllDepartments();
            if (FlowId != null)
            {
                model.FlowSetup.FlowId = Convert.ToInt32(FlowId);
                var flowname = _iFlowSetupManager.GetFlowSetupByFlowId(Convert.ToInt32(FlowId));
                foreach (var item in flowname)
                {
                    FlowSetup final = new FlowSetup();
                    var Dep = _iDepartmentManager.GetDepartmentsById(item.DepartmentId);
                    final.FlowName = Dep.Name;
                    final.FlowSetupIdentity = item.FlowSetupIdentity;
                    model.FlowSetups.Add(final);
                    model.SuccessMessage = "Flow Name has been created successfully!";
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult FlowCreate(FlowSetupViewModel model)
        {
            if (model != null)
            {
               FlowList fs = new FlowList();
                fs = _iFlowListManager.GetFlowListByName(model.FlowList.FlowName);
                if (fs != null)
                {
                    model.FailedMessage = "Flow Name Already Exist!";
                }
                else
                {
                    model.FlowList.UserId = PortalContext.CurrentUser.UserName;
                    model.FlowList.UpdateBy = PortalContext.CurrentUser.UserName;
                    model.FlowList.EntryDate = DateTime.Now;
                    model.FlowList.UpdateDate = DateTime.Now;
                }
                int result = _iFlowListManager.CreateFlowList(model.FlowList);
         
                if (result == 1)
                {
                    model.SuccessMessage = "Flow Name has been created successfully!";

                }
                else
                {
                    model.FailedMessage = "Flow Name creation failed!";
                }

            }

            return RedirectToAction("Index", new { FlowId = model.FlowList.FlowId });
        }
        [HttpPost]
        public ActionResult Index(FlowSetupViewModel model)
        {
            if (model != null)
            {
                FlowSetup fs = new FlowSetup();
                int p = 0;
                int ss = 0;
                //var fn = _iFlowListManager.GetFlowListByFlowId(model.FlowSetup.FlowId.Value).FlowName;
                fs = _iFlowSetupManager.GetFlowSetupByFileId(model.FlowSetup.FlowSetupIdentity);
                if (fs != null)
                {
                   
                    List<SelectionList> flowSetupEditDictionary = model.FlowSetupEditDictionary.Select(x => x.Value).ToList();
                    List<string> selection = new List<string>();
                    foreach (var item in flowSetupEditDictionary)
                    {
                        string dd;
                        dd= item.Value.ToString();
                        selection.Add(dd);
                    }
                    string newform = String.Join(",", selection);
                    string[] oldstatus = fs.IsComplete.Split(',');
                    string[] oldform = fs.FormId.Split(',');
                    string[] newf = newform.Split(',');
                    string[] newstatus = newform.Split(',');
                    string[] newSkipstatus = newform.Split(',');
                    int[] taskSerial = new int[newform.Split(',').Count()]; 
                    int tasksSerial = 0;
                    foreach (var item in newf)
                    {
                        taskSerial[p] = ++tasksSerial;
                        if (oldform.ElementAtOrDefault(p) != null && newf[p] == oldform[p])
                            newstatus[p] = oldstatus[p];
                        else
                        {
                            newstatus[p] = "false";
                        }
                        ++p;
                    }

                    // Skip Status Set
                    string[] oldSkipstatus = fs.SkipStatus.Split(',');
                    foreach (var itemm in newf)
                    {
                        if (oldform.ElementAtOrDefault(ss) != null && newf[ss] == oldform[ss])
                            newSkipstatus[ss] = oldSkipstatus[ss];
                        else
                        {
                            newSkipstatus[ss] = "false";
                        }
                        ++ss;
                    }
                    fs.FormId = newform;
                    fs.SkipStatus = string.Join(",", newSkipstatus);
                    fs.TaskSerial = string.Join(",", taskSerial);
                    fs.DepStartDate = model.FlowSetup.DepStartDate;
                    fs.DepEndDate = model.FlowSetup.DepEndDate;
                    fs.IsComplete = string.Join(",", newstatus);
                    fs.ModifiedBy = PortalContext.CurrentUser.UserName;
                    fs.UpdateDate = DateTime.Now;
                    model.FlowSetup = fs;

                }
                else
                {
                    List<SelectionList> flowSetupEditDictionary = model.FlowSetupEditDictionary.Select(x => x.Value).ToList();
                    List<string> selection = new List<string>();
                    foreach (var item in flowSetupEditDictionary)
                    {
                        string dd;
                        dd = item.Value.ToString();
                        selection.Add(dd);
                    }
                    model.FlowSetup.FormId = String.Join(",", selection);
                    int coun = model.FlowSetup.FormId.Split(',').Count();
                    string[] newDep = model.FlowSetup.FormId.Split(',');
                    string[] deplist = new string[coun];
                    string[] skipstatus = new string[coun];
                    int[] taskSerial = new int[coun];
                    int tasksSerial = 0;
                    for(int i =0; i < coun;i++)
                    {
                        taskSerial[i] = ++tasksSerial;
                        deplist[i] = "false";
                        skipstatus[i] = _iFlowSetupManager.GetTaskStatus(newDep[i], model.FlowSetup.FlowId);
                    }
                    model.FlowSetup.IsComplete = string.Join(",", deplist);
                    model.FlowSetup.SkipStatus = string.Join(",", skipstatus);
                    model.FlowSetup.TaskSerial = string.Join(",", taskSerial);
                    model.FlowSetup.FlowSerial = _iFlowSetupManager.GetFlowSerialNo(model.FlowSetup.FlowId);
                    model.FlowSetup.UserId = PortalContext.CurrentUser.UserName;
                    model.FlowSetup.ModifiedBy = PortalContext.CurrentUser.UserName;
                    model.FlowSetup.EntryDate = DateTime.Now;
                    model.FlowSetup.UpdateDate = DateTime.Now;
                }
           
                int result = _iFlowSetupManager.CreateFlowSetup(model.FlowSetup);
              
                if (result == 1)
                {
                    model.SuccessMessage = "Flow has been created successfully!";

                }
                else
                {
                    model.FailedMessage = "Flow creation failed!";
                }

            }

            return RedirectToAction("Index", new { FlowId = model.FlowSetup.FlowId });
        }
        public ActionResult GetFlow(int FlowId,FlowSetupViewModel model)
        {
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.Department = _iDepartmentManager.GetAllDepartments();
            model.FlowSetup.FlowId = FlowId;
            model.Key = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
            model.FlowSetupEditDictionary.Add(model.Key, new SelectionList());
            model.FormInfos = _iforminfoManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.FormId,
                Text = x.FormName
            }).ToList();
            List<FlowSetup> flow = new List<FlowSetup>();
            if (FlowId > 0)
            {
                flow = _iFlowSetupManager.GetFlowSetupByFlowId(FlowId).OrderBy(x=>x.FlowSerial).ToList();
                foreach (var item in flow)
                {
                    FlowSetup final = new FlowSetup();
                    var Dep = _iDepartmentManager.GetDepartmentsById(item.DepartmentId);
                    final.FlowSetupIdentity = item.FlowSetupIdentity;
                    final.FlowName = Dep.Name;
                    final.FlowSerial = item.FlowSerial;
                    model.FlowSetups.Add(final);

                }
            }
            return View("Index",model);
        }

        public ActionResult FlowUpdate(List<string> flow, int flowId,FlowSetupViewModel model)
        {
          
            foreach (var item in flow)
            {
                var val = item.Trim();
                string[] dt = val.Split(' ');
                string otherArgs = dt[1];
                long depId = _iDepartmentManager.GetDepId(otherArgs);
                long flowsetupIdentity = Convert.ToInt64(dt.Last());
                var flowSequence = dt.First();
                int result = _iFlowSetupManager.UpdateFlowSetup(flowsetupIdentity, depId, flowId, flowSequence);
                if (result == 1)
                {
                    model.SuccessMessage = "File Updated successfully!";

                }
                else
                {
                    model.FailedMessage = "Flow Update failed!";
                }
            }
            return View("Index",model);
        }

        public ActionResult GetFlowWork(string flowDetails, int flowId, FlowSetupViewModel model)
        {
         
            var val = flowDetails.Trim();
            string[] dt = val.Split(' ');
            long flowsetupIdentity = Convert.ToInt64(dt.Last());
            FlowSetup flowValue = _iFlowSetupManager.GetFlowSetupByFileId(flowsetupIdentity);
            string[] formId = flowValue.FormId.Split(',');
            string[] isComplete = flowValue.IsComplete.Split(',');
            string[] isSkip = flowValue.SkipStatus.Split(',');
            List<FormInfo> formInfos = new List<FormInfo>();

            int i = 0;
            foreach (var item in formId)
            {
                FormInfo olddata = _iforminfoManager.FindbyId(Convert.ToInt64(item));
                olddata.FormSerial = dt.First();
                //  olddata.IsComplete = isComplete.First();
                olddata.IsComplete = Convert.ToBoolean(isComplete[i]);
                olddata.IsSkip = Convert.ToBoolean(isSkip[i]);
                olddata.FlowIdentity = flowsetupIdentity.ToString();
                formInfos.Add(olddata);
                ++i;
            }
            return Json(formInfos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeptDelete(long? identity, string flowid,FlowSetupViewModel model)
        {
            _iFlowSetupManager.DeleteWithOrderby(identity, flowid);
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();
            model.Department = _iDepartmentManager.GetAllDepartments();
            model.FlowSetup.FlowId = Convert.ToInt32(flowid);
            var flowname = _iFlowSetupManager.GetFlowSetupByFlowId(Convert.ToInt32(flowid));
            foreach (var item in flowname)
            {
                FlowSetup final = new FlowSetup();
                var Dep = _iDepartmentManager.GetDepartmentsById(item.DepartmentId);
                final.FlowName = Dep.Name;
                final.FlowSetupIdentity = item.FlowSetupIdentity;
                model.FlowSetups.Add(final);
                model.SuccessMessage = "Dept Deleted successfully!";
            }
            return View("Index",model);
        }
        // Tree menu Index 
        public ActionResult TreeMenuIndex(FlowSetupViewModel model)
        {
            model.ChartofAccounts = _iFlowSetupManager.GetChartOfAccount();
            return View(model);
        }
         [HttpGet]
        public JsonResult GetFlowwiseDept(int fileName)
        {
            List<SelectionList> flowSetupList = new List<SelectionList>();
            List<FlowSetup> floseSetups = _iFlowSetupManager.GetFlowSetupByFlowId(fileName).OrderBy(x=>x.FlowSerial).ToList();
            flowSetupList = floseSetups.Select(x => new SelectionList()
            {
                Text = x.Department.Name + " "+ x.FlowSerial,
                Value = Convert.ToInt64(x.FlowSetupIdentity)
            }).ToList();

            return Json(flowSetupList, JsonRequestBehavior.AllowGet);
        }
         [HttpGet]
         public ActionResult GetTaskList(long id,int flowId,FlowSetupViewModel model)
         {
             model.FormInfos = _iforminfoManager.GetAll().Select(x => new SelectionList()
             {
                 Value = x.FormId,
                 Text = x.FormName
             }).ToList();
             model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
             {
                 Value = x.DemandId,
                 Text = x.FileNo
             }).ToList();

             List<FlowSetup> floseSetups = _iFlowSetupManager.GetFlowSetupByFlowId(flowId).OrderBy(x => x.FlowSerial).ToList();
             model.Department = floseSetups.Select(x => new Department()
             {
                 Name = x.Department.Name + " " + x.FlowSerial,
                 DepartmentId = Convert.ToInt64(x.FlowSetupIdentity)
             }).ToList();
             model.FlowSetup = _iFlowSetupManager.GetFlowSetupByFileId(id);
             
             string[] taskid = model.FlowSetup.FormId.Split(',');
             string[] skipStatus = model.FlowSetup.SkipStatus.Split(',');
             string[] comStatus = model.FlowSetup.IsComplete.Split(',');
             int p = 0;
             foreach (var s in taskid)
             {
                
                 string time = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000000000);
                 time = time + Convert.ToString(p);
                 model.Key = time;
                 var flowSetup = new SelectionList();
                 flowSetup.Text = id.ToString();
                 flowSetup.Value = Convert.ToInt64(s);
                 flowSetup.SkipStatus = Convert.ToBoolean(skipStatus[p]);
                 flowSetup.CompleteStatus = Convert.ToBoolean(comStatus[p]);
                 //flowSetup.StartDate = flowseSetup.DepStartDate;
                 //flowSetup.EndDate = flowseSetup.DepEndDate;
                 model.FlowSetupEditDictionary.Add(model.Key, flowSetup);
                 p++;
             }
         
             List<FlowSetup> flow = new List<FlowSetup>();
             if (flowId > 0)
             {
                 flow = _iFlowSetupManager.GetFlowSetupByFlowId(flowId).OrderBy(x => x.FlowSerial).ToList();
                 foreach (var item in flow)
                 {
                     FlowSetup final = new FlowSetup();
                     var Dep = _iDepartmentManager.GetDepartmentsById(item.DepartmentId);
                     final.FlowSetupIdentity = item.FlowSetupIdentity;
                     final.FlowName = Dep.Name;
                     final.FlowSerial = item.FlowSerial;
                     model.FlowSetups.Add(final);

                 }
             }
             model.FlowSetup.FlowId = flowId;
             model.DeptId = id;
             return View("Index",model);
         }
         public ActionResult AddNewRow(FlowSetupViewModel model)
         {
             model.Key = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
             model.FlowSetupEditDictionary.Add(model.Key, new SelectionList());
             model.FormInfos = _iforminfoManager.GetAll().Select(x => new SelectionList()
             {
                 Value = x.FormId,
                 Text = x.FormName
             }).ToList();
             return PartialView("_rowCreate", model);
         }
    
	}
}