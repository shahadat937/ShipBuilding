using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using RMS.Web.Models.ViewModel;
using RMS.BLL.IManager;
using RMS.Model;
using Path = System.IO.Path;
using Microsoft.Reporting.WebForms;
using RMS.Utility;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;

namespace RMS.Web.Controllers
{
    public class FileMovementController : Controller
    {
        private readonly IProjectManager _iProjectManager;
        private readonly IFileManager _iFileManager;
        private readonly IDepartmentManager _iDepartmentManager;
        private readonly IStaffRequirementManager _iStaffRequirementManager;
        private readonly IFlowSetupManager _iFlowSetupManager;
        private readonly IFlowListManager _iFlowListManager;
        private readonly IFileMovementManager _iFileMovementManager;
        private readonly IProjectMovementManager _iProjectMovementManager;
        private readonly IDemandManager _demandManager;
        public FileMovementController(IFileMovementManager iFileMovementManager, IProjectManager iProjectManager, IFileManager iFileManager, IDepartmentManager iDepartmentManager, IStaffRequirementManager iStaffRequirementManager, IFlowSetupManager iFlowSetupManager, IFlowListManager iFlowListManager, IProjectMovementManager iProjectMovementManager, IDemandManager demandManager)
        {
            _iProjectManager = iProjectManager;
            _iFileManager = iFileManager;
            _iDepartmentManager = iDepartmentManager;
            _iStaffRequirementManager = iStaffRequirementManager;
            _iFlowSetupManager = iFlowSetupManager;
            _iFlowListManager = iFlowListManager;
            _iFileMovementManager = iFileMovementManager;
            _iProjectMovementManager = iProjectMovementManager;
            _demandManager = demandManager;
        }
        public ActionResult Index(string ProjectId, FlowSetupViewModel model)
        {
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();

            if (ProjectId != null)
            {
                model.FlowSetup.FlowId =Convert.ToInt64(ProjectId);
            }
            return View(model);
        }

        public ActionResult ProjectStatus(string ProjectId,FlowSetupViewModel model)
        {
            model.ProjectList = _demandManager.GetAll().Select(x => new SelectionList()
            {
                Value = x.DemandId,
                Text = x.FileNo
            }).ToList();

            if (ProjectId != null)
            {
                model.FlowSetup.FlowId = Convert.ToInt64(ProjectId);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(FileMovementViewModel model)
        {
           
            long flowIdentity = 0;

            if (model.FlowSetup.FlowId > 0)
            {
                int proId = (int) model.FlowSetup.FlowId;
                var FlowList = _iFlowSetupManager.GetFlowSetupByFlowId(proId).OrderBy(x => x.FlowSerial).ToList();
                for (int i = 0; i < FlowList.Count() && flowIdentity == 0; i++)
                {

                    string[] com = FlowList[i].IsComplete.Split(',');
                  

                    foreach (var item in com)
                    {
                        if (item.ToLower() != "true" && FlowList[i].IsSkip != true)
                        {
                            flowIdentity = FlowList[i].FlowSetupIdentity;
                            break;
                        }
                 
                    }
                }
            }
            if (flowIdentity > 0)
            {
                FlowSetup oldData = _iFlowSetupManager.GetFlowSetupByIdentity(flowIdentity);
                oldData.IsSkip = true;
                oldData.SkipRemarks = model.FlowSetup.SkipRemarks;
               int re = _iFlowSetupManager.CreateFlowSetup(oldData);
            }
            return RedirectToAction("Index", new { ProjectId = model.FlowSetup.FlowId });
        }
        public ActionResult OngoingProject(FileMovementViewModel model)
        {
            var all = _demandManager.GetAll().Where(x => x.ProjectType == 6).ToList();
            var ss = _demandManager.GetCompleteProject().ToList();
            var query = from file in all
                        where !ss.Any(employee => employee.DemandId == file.DemandId)
                        select file;
            model.Demands = query.ToList();
            return View(model);
        }
        public ActionResult FutureProject(FileMovementViewModel model)
        {
            model.Demands = _demandManager.GetAll().Where(x => x.ProjectType == 7).ToList();
            return View(model);
        }

        public ActionResult AllProjectList(FileMovementViewModel model)
        {
            model.Demands = _demandManager.GetAll().ToList();
            return View(model);
        }

        public ActionResult CompleteProject(FileMovementViewModel model)
        {
            model.Demands = _demandManager.GetCompleteProject().ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GetFlow(int ProjectId)
        {
          
            List<vwFileStatus> fileStatuses =new List<vwFileStatus>();
            int depWorkComplete = 1;
           
            if (ProjectId > 0)
            {
                var FlowList = _iFlowSetupManager.GetFlowSetupByFlowId(ProjectId).OrderBy(x =>x.FlowSerial);
                foreach (var flowSetup in FlowList)
                {
                    vwFileStatus model = new vwFileStatus();
                    model.DepartmentName = flowSetup.Department.Name;
                    string[] com = flowSetup.IsComplete.Split(',');
                    int anyTrue = 0;
                    foreach (var item in com)
                     {
                            if (item.ToLower() != "true")
                                depWorkComplete = 0;
                            else
                            {
                                depWorkComplete = 1;
                                anyTrue = 1;
                            }
                        } 
                   
                  
                    model.FileStatus = depWorkComplete;
                    model.AnyTrue = anyTrue;
                    model.IsSkip = flowSetup.IsSkip;
                    fileStatuses.Add(model);
                }
            }
            return Json(fileStatuses, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PendingFileDownload(string ProjectId)
        {
            long CurrentFileID = Convert.ToInt64(ProjectId);
            string CurrentFileName = "";

            //string contentType = string.Empty;
            string contentType = "application/pdf";
            if (CurrentFileID != null)
            {
                var filesCol1 = _iFileMovementManager.GetFileMovementByProjectId(CurrentFileID).Where(x => x.Status == 0);
                if (filesCol1.Count() > 0)
                {
                    var filesCol = filesCol1.First().FileURL;
                    //DirectoryInfo CurrentFileName1 = new DirectoryInfo(HttpContext.Server.MapPath(filesCol.ToString()));
                    CurrentFileName = Path.Combine(Server.MapPath(filesCol.ToString()));
                    if (System.IO.File.Exists(CurrentFileName))
                    {

                        if (CurrentFileName.Contains(".pdf"))
                        {
                            contentType = "application/pdf";
                        }

                        else if (CurrentFileName.Contains(".docx"))
                        {
                            contentType = "application/docx";
                        }
                        else if (CurrentFileName.Contains(".doc"))
                        {
                            contentType = "application/doc";
                        }
                        else if (CurrentFileName.Contains(".dwg"))
                        {
                            contentType = "application/dwg";
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    //return File(CurrentFileName, contentType, CurrentFileName);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return File(CurrentFileName, contentType, CurrentFileName);
        }
        public ActionResult PendingFileDownload1(FileMovementViewModel model)
        {
            long CurrentFileID = Convert.ToInt64(model.FileMovement.ProjectId);
            string CurrentFileName = "";

            //string contentType = string.Empty;
            string contentType = "application/pdf";
            if (CurrentFileID != null)
            {
                var filesCol1 =_iFileMovementManager.GetFileMovementByProjectId(CurrentFileID).Where(x => x.Status == 0);
                if (filesCol1.Count()>0)
                {
                    var filesCol = filesCol1.First().FileURL;
                    //DirectoryInfo CurrentFileName1 = new DirectoryInfo(HttpContext.Server.MapPath(filesCol.ToString()));
                    CurrentFileName = Path.Combine(Server.MapPath(filesCol.ToString()));
                    if (System.IO.File.Exists(CurrentFileName))
                    {

                        if (CurrentFileName.Contains(".pdf"))
                        {
                            contentType = "application/pdf";
                        }

                        else if (CurrentFileName.Contains(".docx"))
                        {
                            contentType = "application/docx";
                        }
                        else if (CurrentFileName.Contains(".doc"))
                        {
                            contentType = "application/doc";
                        }
                        else if (CurrentFileName.Contains(".dwg"))
                        {
                            contentType = "application/dwg";
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    //return File(CurrentFileName, contentType, CurrentFileName);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return File(CurrentFileName, contentType, CurrentFileName);
        }
        public ActionResult FileHistory( string ProjectId)
        {
            FileMovementViewModel model = new FileMovementViewModel();
            //RDLC prn = RDLC();
            var localReport = new LocalReport();
            long CurrentFileID = Convert.ToInt64(ProjectId);
            //if (CurrentFileID != null)
            //{
                var pms = _iProjectMovementManager.GetProjectMovementByProjectId(CurrentFileID).ToList();
                var fms = _iFileMovementManager.GetFileMovementByProjectId(CurrentFileID).Select(x => new { ProjectId = Convert.ToInt64(x.ProjectId ?? 0), DepartmentFrom = x.DepartmentFrom.ToString(), TranferDate = x.TranferDate, ReceiveDate = x.ReceiveDate, DepExpireDate = x.DepExpireDate, FileURL = x.FileURL }).ToList();
                //var fms = _iFileMovementManager.GetFileMovementByProjectId(CurrentFileID).ToList();
                var pfms = (from c in pms join d in fms on new { a=(long?)c.ProjectIdentity, b=(string)c.DepartmentFrom } equals new { a=(long?)d.ProjectId, b=(string)d.DepartmentFrom } select new { SerialNo = c.SerialNo, ProjectIdentity = c.ProjectIdentity, StuffRequirementId = c.StuffRequirementId, ProjectId = c.ProjectId, ProjectName = c.ProjectName, ProjectValue = c.ProjectValue, ProjectDescription = c.ProjectDescription, Status = c.Status, PStatusName = c.PStatusName, ProjectFlow = c.ProjectFlow, DepartmentList = c.DepartmentList, FlowId = c.FlowId, DepartmentId = c.DepartmentId, Milestone = c.Milestone, DepartmentFrom = c.DepartmentFrom, DepartmentTo = c.DepartmentTo, FMStatusId = c.FMStatusId, FMStatus = c.FMStatus, TranferDate = d.TranferDate, ReceiveDate = d.ReceiveDate, DepExpireDate = d.DepExpireDate, FileURL = d.FileURL }).ToList();


                var reportDataSource = new ReportDataSource { Name = "DataSet1" };
                localReport.DataSources.Add(reportDataSource);
                reportDataSource.Value = pfms;

                localReport.ReportPath = Server.MapPath("~/Reports/FileMovementInformation.rdlc");
                string mimeType;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                byte[] renderedBytes = localReport.Render("PDF", "", out mimeType, out encoding,
                    out fileNameExtension, out streams, out warnings);
                Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);
                return File(renderedBytes, fileNameExtension);
            //}
            //return File(renderedBytes, fileNameExtension);
        }
        public ActionResult DownloadAll(string ProjectId)
        {
            //FileDownloads obj = new FileDownloads();
            string CurrentFileName = "";
            int CurrentFileID = Convert.ToInt32(ProjectId);
            var filesCol1 = _iFileMovementManager.GetFileMovementByProjectId(CurrentFileID).ToList();
            var memoryStream = new MemoryStream();
            string projectName = _iProjectManager.GetProjectByIdentity(CurrentFileID).ProjectName.ToString();
            if (filesCol1.Count() > 0)
            {
                using (var ziparchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    for (int i = 0; i < filesCol1.Count; i++)
                    {
                        CurrentFileName = Path.Combine(Server.MapPath(filesCol1[i].FileURL.ToString()));
                        string filename = filesCol1[i].FileURL.ToString();
                        if (System.IO.File.Exists(CurrentFileName))
                        {
                            ziparchive.CreateEntryFromFile(CurrentFileName, filename.Remove(0, 2));
                        }
                    }
                }
            }
            return File(memoryStream.ToArray(), "application/zip", projectName+".zip");
        }
	}
}