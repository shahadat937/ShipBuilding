using System;
using System.Linq;
using System.Web.Mvc;
using RMS.BLL.IManager;
using RMS.Utility;
using RMS.Web.Models.ViewModel;
using System.IO;

namespace RMS.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IProjectManager _iProjectManager;
        private readonly IFileManager _iFileManager;
        private readonly IDepartmentManager _iDepartmentManager;
        private readonly IStaffRequirementManager _iStaffRequirementManager;
        public FileController(IProjectManager iProjectManager, IFileManager iFileManager, IDepartmentManager iDepartmentManager, IStaffRequirementManager iStaffRequirementManager)
        {
            _iProjectManager = iProjectManager;
            _iFileManager = iFileManager;
            _iDepartmentManager = iDepartmentManager;
            _iStaffRequirementManager = iStaffRequirementManager;
        }
        public ActionResult Index(FileViewModel model)
        {
            model.Files = _iFileManager.GetFiles();
            return View(model);
        }
        public ActionResult SearchByKey(FileViewModel model)
        {
            string searchKey = model.File.FileName;
            if (searchKey == null)
            {
                model.Files = _iFileManager.GetFiles();
            }
            else
            {
                searchKey = searchKey.ToLower();
                model.Files = _iFileManager.GetFiles().Where(x => x.FileName.ToLower().Contains(searchKey)).ToList();
                if (!model.Files.Any())
                {
                    //model.SuccessMessage = 0;
                    model.FailedMessage = "Data is not found.";
                }
            }
            return View("Index", model);
        }
        public ActionResult Create(string FileId, FileViewModel model, string projectId)
        {
            model.StaffReqierment = _iStaffRequirementManager.GetStaffRequirement(projectId);
            model.Department = _iDepartmentManager.GetAllDepartments();
            if (FileId != null)
            {
                model.File = _iFileManager.GetFilesByFileId(Convert.ToInt64(FileId));
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(FileViewModel model)
        {
            if (model != null)
            {

                string _FileName = "";
                string _path = "";
                string _fileUrl = "";
                //if (ModelState.IsValid)
                //{
                var FileTypes = new string[]{    
                    ".pdf",
                    ".docx",
                    ".doc",
                    ".dwg"
                };

                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var fileExtention = Path.GetExtension(file.FileName);
                    if (!FileTypes.Contains(fileExtention))
                    {
                        model.FailedMessage = "Please Chose Word, PDF & DWG Format!";
                    }
                    else
                    {
                        _FileName = model.File.StuffRequirementId + "_" + Path.GetFileName(file.FileName);
                        _path = Path.Combine(Server.MapPath("~/File"), _FileName);
                        file.SaveAs(_path);
                        _fileUrl = "~/File/" + _FileName;


                        RMS.Model.File f = new RMS.Model.File();
                        f = _iFileManager.GetFilesByFileId(model.File.FileId);
                        if (f != null)
                        {

                            f.StuffRequirementId = model.File.StuffRequirementId;
                            f.FileCode = model.File.FileCode;
                            f.FileName = model.File.FileName;
                            f.DepartmentId = model.File.DepartmentId;
                            f.FleStetus = model.File.FleStetus;
                            if (_fileUrl.Length > 0)
                            {
                                f.FileUrl = _fileUrl;
                            }
                            f.UpdatedBy = PortalContext.CurrentUser.UserName;
                            f.UploadDate = DateTime.Now;
                            model.File = f;

                        }
                        else
                        {
                            //}

                            model.File.FileUrl = _fileUrl;
                            model.File.ProjectId = 1;
                            model.File.UserId = PortalContext.CurrentUser.UserName;
                            model.File.UpdatedBy = PortalContext.CurrentUser.UserName;
                            model.File.EntyDate = DateTime.Now;
                            model.File.LastUpdate = DateTime.Now;
                            model.File.UploadDate = DateTime.Now;
                        }
                        int result = _iFileManager.CreateFile(model.File);
                        if (result == 1)
                        {
                            model.SuccessMessage = "Operation Successful!";
                        }
                        else
                        {
                            model.FailedMessage = "Operation Failed!";
                        }
                    }
                }

                
            }

            return View(model);
        }
        public FileResult Download(string FileID)
        {
            int CurrentFileID = Convert.ToInt32(FileID);
            var filesCol = _iFileManager.GetFilesByFileId(CurrentFileID).FileUrl;
            //DirectoryInfo CurrentFileName1 = new DirectoryInfo(HttpContext.Server.MapPath(filesCol.ToString()));
            string CurrentFileName = Path.Combine(Server.MapPath(filesCol.ToString()));

            string contentType = string.Empty;

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
            return File(CurrentFileName, contentType, CurrentFileName);
        }   

        public ActionResult ViewFile()
        {
            var model = new FileViewModel();
            model.Projects = _iProjectManager.GetCurrentProjects();
            
            return View(model);
        }
        public ActionResult GetFilesByProjectId(string projectId,FileViewModel model)
        {

            model.Files = _iFileManager.GetFilesByProjectId(Convert.ToInt64(projectId));

            return View("~/Views/File/_FileList.cshtml", model);
        }
    }
}