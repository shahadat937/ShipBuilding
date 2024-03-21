using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.Manager
{
    public class NSPCMeetingWorkManager : BaseManager, INSPCMeetingWorkManager
    {
        private readonly INSPCMeetingWorkRepository _iNSPCMeetingWorkRepository;
        public NSPCMeetingWorkManager(INSPCMeetingWorkRepository iNSPCMeetingWorkRepository)
        {
            _iNSPCMeetingWorkRepository = iNSPCMeetingWorkRepository;
        }
        public List<NSPCMeetingWork> GetAllNSPCMeetingWork()
        {
            return _iNSPCMeetingWorkRepository.All().ToList();
        }
        public List<NSPCMeetingWork> GetNSPCMeetingWorksBySearchKey(string searchKey)
        {
            return _iNSPCMeetingWorkRepository.Filter(x => x.FileNo.ToLower().Contains(searchKey.ToLower()) ||       
                x.ToAuthority.ToLower().Contains(searchKey.ToLower())).ToList();
        }
        public string SaveNSPCMeetingWork(NSPCMeetingWork model)
        {
            string _FileName = "";
            string _path = "";
            string _fileUrl = "";
            string message = "";
            var TenderDocumentTypes = new string[] { ".pdf", ".docx", ".doc" };
            var tenderDocument = System.Web.HttpContext.Current.Request.Files[0];
            if (tenderDocument != null && tenderDocument.ContentLength > 0)
            {
                var tenderName = Path.GetFileName(tenderDocument.FileName);
                var documentExtension = Path.GetExtension(tenderDocument.FileName);
                if (!TenderDocumentTypes.Contains(documentExtension))
                {
                    message = "Please Chose Word or PDF Format!";
                }
                else
                {
                    _FileName = model.FileNo + "" + Path.GetFileName(tenderDocument.FileName);
                    _path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/NSPCMeetingWorkUploadedDocument"), _FileName);
                    tenderDocument.SaveAs(_path);
                    _fileUrl = "~/NSPCMeetingWorkUploadedDocument/" + "" + _FileName;
                    NSPCMeetingWork nspcMeetingWork = new NSPCMeetingWork();
                    nspcMeetingWork.FileNo = Convert.ToString(model.FileNo);
                    nspcMeetingWork.FileUrl = _fileUrl;
                    nspcMeetingWork.CategoryId = model.CategoryId;
                    nspcMeetingWork.OfficerName = model.OfficerName;
                    nspcMeetingWork.DescriptionBody = model.DescriptionBody;
                    nspcMeetingWork.ToAuthority = model.ToAuthority;
                    nspcMeetingWork.IssueDate = model.IssueDate;
                    nspcMeetingWork.CreatedBy = PortalContext.CurrentUser.UserName;
                    nspcMeetingWork.CreatedDate = DateTime.Now;
                    nspcMeetingWork.UpdatedBy = PortalContext.CurrentUser.UserName;
                    nspcMeetingWork.UpdatedDate = DateTime.Now;
                    int result = _iNSPCMeetingWorkRepository.Save(nspcMeetingWork);
                    if (result > 0)
                    {
                        message = "Successfully created NSPC Meeting Work ";
                    }
                    else
                    {
                        message = "NSPC Meeting Work creation failed";
                    }
                }
            }
            return message;
        }
        public NSPCMeetingWork GetNSPCMeetingWorkById(long nspcMeetingWorkId)
        {
            return _iNSPCMeetingWorkRepository.FindOne(x => x.NSPCMeetingWorkId == nspcMeetingWorkId);
        }
        public string EditNSPCMeetingWork(NSPCMeetingWork model)
        {
            NSPCMeetingWork nspcMeetingWork = new NSPCMeetingWork();
            long nspcMeetingWorkId = Convert.ToInt64(model.NSPCMeetingWorkId);
            nspcMeetingWork = _iNSPCMeetingWorkRepository.FindOne(x => x.NSPCMeetingWorkId == nspcMeetingWorkId);
            string _TenderName = "";
            string _path = "";
            string _tenderUrl = "";
            string message = "";
            var TenderDocumentTypes = new string[] { ".pdf", ".docx", ".doc" };
            var tenderDocument = System.Web.HttpContext.Current.Request.Files[0];
            if (tenderDocument != null && tenderDocument.ContentLength > 0)
            {
                var tenderName = Path.GetFileName(tenderDocument.FileName);
                var documentExtension = Path.GetExtension(tenderDocument.FileName);
                if (!TenderDocumentTypes.Contains(documentExtension))
                {
                    message = "Please Chose Word or PDF Format!";
                }
                else
                {
                    _TenderName = model.FileNo + "" + Path.GetFileName(tenderDocument.FileName);
                    _path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/FinancialOfferUploadedDocument"), _TenderName);
                    tenderDocument.SaveAs(_path);
                    _tenderUrl = "~/FinancialOfferUploadedDocument/" + "" + _TenderName;
                    nspcMeetingWork.FileNo = model.FileNo;
                    nspcMeetingWork.CategoryId = model.CategoryId;
                    nspcMeetingWork.OfficerName = model.OfficerName;
                    nspcMeetingWork.DescriptionBody = model.DescriptionBody;
                    nspcMeetingWork.ToAuthority = model.ToAuthority;
                    if (_tenderUrl.Length > 0)
                    {
                        nspcMeetingWork.FileUrl = _tenderUrl;
                    }
                    nspcMeetingWork.IssueDate = model.IssueDate;
                    nspcMeetingWork.UpdatedBy = PortalContext.CurrentUser.UserName;
                    nspcMeetingWork.UpdatedDate = DateTime.Now;
                    int result = _iNSPCMeetingWorkRepository.Edit(nspcMeetingWork);
                    if (result > 0)
                    {
                        message = "Successfully Edited document";
                    }
                    else
                    {
                        message = "Edit failed";
                    }
                }
            }
            else
            {
                nspcMeetingWork.FileNo = model.FileNo;
                nspcMeetingWork.CategoryId = model.CategoryId;
                nspcMeetingWork.OfficerName = model.OfficerName;
                nspcMeetingWork.DescriptionBody = model.DescriptionBody;
                nspcMeetingWork.ToAuthority = model.ToAuthority;

                nspcMeetingWork.IssueDate = model.IssueDate;
                nspcMeetingWork.UpdatedBy = PortalContext.CurrentUser.UserName;
                nspcMeetingWork.UpdatedDate = DateTime.Now;

                int result =_iNSPCMeetingWorkRepository.Edit(nspcMeetingWork);
                if (result > 0)
                {
                    message = "Successfully Edited document";
                }
                else
                {
                    message = "Edit failed";
                }
            }
            return message;
        }
        public int DeleteNSPCMeetingWork(NSPCMeetingWork model)
        {
            return _iNSPCMeetingWorkRepository.Delete(model);
        }
    }
}
