using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using log4net.Util.TypeConverters;
using Newtonsoft.Json;
using RMS.BLL.IManager;
using RMS.Model;
using RMS.Utility;
using RMS.Web.Models.ViewModel;

namespace RMS.Web.Controllers
{
    [Authorize]
    public class NoticeController : Controller
    {
        private INoticeManager _noticeManager;
        private IFlowSetupManager _flowSetupManager;
        public NoticeController(INoticeManager noticeManager, IFlowSetupManager flowSetupManager)
        {
            _noticeManager = noticeManager;
            _flowSetupManager = flowSetupManager;
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult Index(string projectId,string projectName,NoticeViewModel model)
        {
            //model.Notices = _noticeManager.GetNotices();
            model.ProjectId = projectId;
            model.ProjectName = projectName;
            return View(model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult Edit(string noticeIdentity, NoticeViewModel model)
        {
            if (noticeIdentity != null)
            {
                model.Notice = _noticeManager.FindOne(Convert.ToInt32(noticeIdentity));
            }
            return View(model);
        }

        public JsonResult GetProjectStatus(string projectId)
        {
            List<ProjectStatusForGanttChart> projectStatusForGanttCharts = new List<ProjectStatusForGanttChart>();
            var pid = Convert.ToInt64(projectId);
            var flowdata = _flowSetupManager.GetAll().Where(x => x.FlowId == pid).ToList();
            foreach (var flowSetup in flowdata)
            {
                if (flowSetup.IsSkip != true)
                {
                       ProjectStatusForGanttChart projectStatusForGantt = new ProjectStatusForGanttChart();
                projectStatusForGantt.TaskName = flowSetup.Department.Name;
                projectStatusForGantt.StartDate = (int)(Convert.ToDateTime(flowSetup.DepEndDate).Date - Convert.ToDateTime(flowSetup.DepStartDate).Date).TotalDays;
                if (flowSetup.ActDepEndDate != null)
                    projectStatusForGantt.EndDate =
                        (int)
                            (Convert.ToDateTime(flowSetup.ActDepEndDate).Date -
                             Convert.ToDateTime(flowSetup.DepStartDate).Date).TotalDays;
                else
                    projectStatusForGantt.EndDate = 0;
                projectStatusForGanttCharts.Add(projectStatusForGantt);
                }
             
            }

            return Json(projectStatusForGanttCharts, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult Save(NoticeViewModel model)
        {
            //string imageUrl = "";
            //if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
            //{
            //    var validImageTypes = new string[]
            //{
            //    "image/gif",
            //    "image/jpeg",
            //    "image/pjpeg",
            //    "image/png",
            //    "pdf"
            //};
            //    long maxId = 0;
            //    var uploadDir = "~/uploads";
            //    string file = "_" + model.ImageUpload.FileName;

            //    var imagePath = Path.Combine(Server.MapPath(uploadDir), file);
            //    imageUrl = Path.Combine(uploadDir, file);
            //    model.ImageUpload.SaveAs(imagePath);
            //}
            model.Notice.Status = true;
            //model.Notice.BranchInfoIdentity = PortalContext.CurrentUser.BranchInfoIdentity;
            ResponseModel response = _noticeManager.Save(model.Notice);
            model.Message = response.Message;
            model.IsSucceed = response.ResponsStatus;
            return View("Edit", model);
        }
        [Authorize(Roles = "AdminUser,RemittanceDivisionOperationUser,BEFTNUser,OperationAdmin")]
        public ActionResult Delete(string noticeIdentity, NoticeViewModel model)
        {
            if (noticeIdentity != null)
            {
                ResponseModel response = _noticeManager.Delete(Convert.ToInt32(noticeIdentity));
            }
            model.Notices = _noticeManager.GetNotices();
            return View("Index", model);
        }
    }
}