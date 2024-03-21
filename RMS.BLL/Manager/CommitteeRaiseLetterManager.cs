using System.Data.Entity;
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
    public class CommitteeRaiseLetterManager : BaseManager, ICommitteeRaiseLetterManager
    {
        private readonly ICommitteeRaiseLetterRepository _iCommitteeRaiseLetterRepository;
        private readonly IFlowSetupManager _flowSetupManager;
        private readonly IFinanceMinistryLetterRepository _financeMinistryLetterRepository;
        private readonly IAFDLetterRepository _afdLetterRepository;
        public CommitteeRaiseLetterManager(ICommitteeRaiseLetterRepository iCommitteeRaiseLetterRepository, IFlowSetupRepository ifFlowSetupRepository, IFlowSetupManager flowSetupManager, IFinanceMinistryLetterRepository financeMinistryLetterRepository, IAFDLetterRepository afdLetterRepository)
        {
            _iCommitteeRaiseLetterRepository = iCommitteeRaiseLetterRepository;
            _flowSetupManager = flowSetupManager;
            _financeMinistryLetterRepository = financeMinistryLetterRepository;
            _afdLetterRepository = afdLetterRepository;
        }
        public string SaveCommitteeRaiseLetter(CommitteeRaiseLetter model, string dirFullPath, string flowSerial, string formId)
        {
            string _TenderName = "";
            string _tenderpath = "";
            string _docPath = "";
            string message = "";
            string approveDoc = "";
           
            var TenderDocumentTypes = new string[] { ".pdf", ".docx", ".doc" };
            var tenderDocument = System.Web.HttpContext.Current.Request.Files[0];
            //var approveDocument = System.Web.HttpContext.Current.Request.Files[1];
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
                    string[] files;
                    int numFiles;
                    long maxId = 0;

                    files = System.IO.Directory.GetFiles(dirFullPath);
                    numFiles = files.Length;
                    if (numFiles > 0)
                        maxId = numFiles + 1;
                    _TenderName = maxId + "" + Path.GetFileName(tenderDocument.FileName);
                     _tenderpath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/File/CommitteRaiseLetter/"), _TenderName);
                    model.AttachedDocument = _tenderpath;
                    tenderDocument.SaveAs(_tenderpath);
                   
                }
            }
           
            switch (formId)
            {
                case "10010": 
                       var oldData =
                _afdLetterRepository.FindOne(x => x.LetterId == model.CommitteeRaiseLetterId);
            if (oldData != null)
            {
                // Flow Status Update
                _flowSetupManager.UpdateFormStatusInFlow(model.FileNo, flowSerial, FormInformation.LettersendtoAFC,null);
             
                oldData.FileNo = model.FileNo;
                oldData.CommonCodeTypeValueId = model.CommonCodeTypeValueId;
                oldData.IssueDate = model.IssueDate;
                oldData.AttachedDocument = model.AttachedDocument ?? oldData.AttachedDocument;
                oldData.FromAuthority = model.FromAuthority;
                oldData.ToAuthority = model.ToAuthority;
                oldData.InformedAuthority = model.InformedAuthority;
                //oldData.IsApprove = model.IsApprove;
                //oldData.ApproveUrl = model.ApproveUrl ?? oldData.ApproveUrl;
                oldData.Remarks = model.Remarks;
                oldData.UpdatedBy = PortalContext.CurrentUser.UserName;
                oldData.UpdatedDate = DateTime.Now;
                int result = _afdLetterRepository.Edit(oldData);
                if (result > 0)
                {
                    message = "Successfully uploaded document";
                }
                else
                {
                    message = "Upload failed";
                }
            }
            else
            {
                _flowSetupManager.UpdateFormStatusInFlow(model.FileNo, flowSerial, FormInformation.LettersendtoAFC, DateTime.Now);

                AFDLetter committeeRaiseLetter = new AFDLetter();
                committeeRaiseLetter.FileNo = model.FileNo;
                committeeRaiseLetter.CommonCodeTypeValueId = model.CommonCodeTypeValueId;
                committeeRaiseLetter.IssueDate = model.IssueDate;
                committeeRaiseLetter.AttachedDocument = model.AttachedDocument;
                committeeRaiseLetter.FromAuthority = model.FromAuthority;
                committeeRaiseLetter.ToAuthority = model.ToAuthority;
                committeeRaiseLetter.InformedAuthority = model.InformedAuthority;
                //committeeRaiseLetter.IsApprove = model.IsApprove;
                //committeeRaiseLetter.ApproveUrl = model.ApproveUrl;
                committeeRaiseLetter.Remarks = model.Remarks;
                committeeRaiseLetter.CreatedBy = PortalContext.CurrentUser.UserName;
                committeeRaiseLetter.CreatedDate = DateTime.Now;
                committeeRaiseLetter.UpdatedBy = PortalContext.CurrentUser.UserName;
                committeeRaiseLetter.UpdatedDate = DateTime.Now;
                int result = _afdLetterRepository.Save(committeeRaiseLetter);
                if (result > 0)
                {
                    message = "Successfully uploaded document";
                }
                else
                {
                    message = "Upload failed";
                }
            }

                    break;
                case "10016": 
                       var oldDataa =
                _financeMinistryLetterRepository.FindOne(x => x.LetterId == model.CommitteeRaiseLetterId);
            if (oldDataa != null)
            {
                // Flow Status Update
                _flowSetupManager.UpdateFormStatusInFlow(model.FileNo, flowSerial, FormInformation.LettersendtoFinanceMinistry,null);

                oldDataa.FileNo = model.FileNo;
                oldDataa.CommonCodeTypeValueId = model.CommonCodeTypeValueId;
                oldDataa.IssueDate = model.IssueDate;
                oldDataa.AttachedDocument = model.AttachedDocument ?? oldDataa.AttachedDocument;
                oldDataa.FromAuthority = model.FromAuthority;
                oldDataa.ToAuthority = model.ToAuthority;
                oldDataa.InformedAuthority = model.InformedAuthority;
                oldDataa.IsApprove = model.IsApprove;
                oldDataa.ApproveUrl = model.ApproveUrl ?? oldDataa.ApproveUrl;
                oldDataa.Remarks = model.Remarks;
                oldDataa.UpdatedBy = PortalContext.CurrentUser.UserName;
                oldDataa.UpdatedDate = DateTime.Now;
                int result = _financeMinistryLetterRepository.Save(oldDataa);
                if (result > 0)
                {
                    message = "Successfully uploaded document";
                }
                else
                {
                    message = "Upload failed";
                }
            }
            else
            {
                _flowSetupManager.UpdateFormStatusInFlow(model.FileNo, flowSerial, FormInformation.LettersendtoFinanceMinistry,DateTime.Now);

                FinanceMinistryLetter committeeRaiseLetter = new FinanceMinistryLetter();
                committeeRaiseLetter.FileNo = model.FileNo;
                committeeRaiseLetter.CommonCodeTypeValueId = model.CommonCodeTypeValueId;
                committeeRaiseLetter.IssueDate = model.IssueDate;
                committeeRaiseLetter.AttachedDocument = model.AttachedDocument;
                committeeRaiseLetter.FromAuthority = model.FromAuthority;
                committeeRaiseLetter.ToAuthority = model.ToAuthority;
                committeeRaiseLetter.InformedAuthority = model.InformedAuthority;
                committeeRaiseLetter.IsApprove = model.IsApprove;
                committeeRaiseLetter.ApproveUrl = model.ApproveUrl;
                committeeRaiseLetter.Remarks = model.Remarks;
                committeeRaiseLetter.CreatedBy = PortalContext.CurrentUser.UserName;
                committeeRaiseLetter.CreatedDate = DateTime.Now;
                committeeRaiseLetter.UpdatedBy = PortalContext.CurrentUser.UserName;
                committeeRaiseLetter.UpdatedDate = DateTime.Now;
                int result = _financeMinistryLetterRepository.Save(committeeRaiseLetter);
                if (result > 0)
                {
                    message = "Successfully uploaded document";
                }
                else
                {
                    message = "Upload failed";
                }
            }

                    break;
                case "10017": 
                       var oldDaata =
                _iCommitteeRaiseLetterRepository.FindOne(x => x.CommitteeRaiseLetterId == model.CommitteeRaiseLetterId);
                       if (oldDaata != null)
            {
                // Flow Status Update
                _flowSetupManager.UpdateFormStatusInFlow(model.FileNo, flowSerial, FormInformation.LettersendtoSFC,null);

                oldDaata.FileNo = model.FileNo;
                oldDaata.CommonCodeTypeValueId = model.CommonCodeTypeValueId;
                oldDaata.IssueDate = model.IssueDate;
                oldDaata.AttachedDocument = model.AttachedDocument ?? oldDaata.AttachedDocument;
                oldDaata.FromAuthority = model.FromAuthority;
                oldDaata.ToAuthority = model.ToAuthority;
                oldDaata.InformedAuthority = model.InformedAuthority;
                oldDaata.IsApprove = model.IsApprove;
                oldDaata.ApproveUrl = model.ApproveUrl ?? oldDaata.ApproveUrl;
                oldDaata.Remarks = model.Remarks;
                oldDaata.UpdatedBy = PortalContext.CurrentUser.UserName;
                oldDaata.UpdatedDate = DateTime.Now;
                int result = _iCommitteeRaiseLetterRepository.Save(oldDaata);
                if (result > 0)
                {
                    message = "Successfully uploaded document";
                }
                else
                {
                    message = "Upload failed";
                }
            }
            else
            {
                _flowSetupManager.UpdateFormStatusInFlow(model.FileNo, flowSerial, FormInformation.LettersendtoSFC, DateTime.Now);

                CommitteeRaiseLetter committeeRaiseLetter = new CommitteeRaiseLetter();
                committeeRaiseLetter.FileNo = model.FileNo;
                committeeRaiseLetter.CommonCodeTypeValueId = model.CommonCodeTypeValueId;
                committeeRaiseLetter.IssueDate = model.IssueDate;
                committeeRaiseLetter.AttachedDocument = model.AttachedDocument;
                committeeRaiseLetter.FromAuthority = model.FromAuthority;
                committeeRaiseLetter.ToAuthority = model.ToAuthority;
                committeeRaiseLetter.InformedAuthority = model.InformedAuthority;
                committeeRaiseLetter.IsApprove = model.IsApprove;
                committeeRaiseLetter.ApproveUrl = model.ApproveUrl;
                committeeRaiseLetter.Remarks = model.Remarks;
                committeeRaiseLetter.CreatedBy = PortalContext.CurrentUser.UserName;
                committeeRaiseLetter.CreatedDate = DateTime.Now;
                committeeRaiseLetter.UpdatedBy = PortalContext.CurrentUser.UserName;
                committeeRaiseLetter.UpdatedDate = DateTime.Now;
                int result = _iCommitteeRaiseLetterRepository.Save(committeeRaiseLetter);
                if (result > 0)
                {
                    message = "Successfully uploaded document";
                }
                else
                {
                    message = "Upload failed";
                }
            }

                    break;
            }
         
            return message;
        }
        public List<CommitteeRaiseLetter> GetCommitteeRaiseLetters()
        {
            return _iCommitteeRaiseLetterRepository.All().ToList();
        }
        public int DeleteCommitteeRaiseLetter(CommitteeRaiseLetter committeeRaiseLetter)
        {
           
            return _iCommitteeRaiseLetterRepository.Delete(committeeRaiseLetter);
        }
       public List<CommitteeRaiseLetter> GetCommitteeRaiseLettersBySearchKey(string searchKey)
        {
            return _iCommitteeRaiseLetterRepository.Filter(x =>x.FromAuthority.ToLower().Contains(searchKey.ToLower()) || x.ToAuthority.ToLower().Contains(searchKey.ToLower()) ||
               x.InformedAuthority.ToLower().Contains(searchKey.ToLower())).ToList();
        }


       public CommitteeRaiseLetter FindCommitteeRaiseLetterById(long committeeRaiseLetterId)
       {
          return _iCommitteeRaiseLetterRepository.FindOne(x => x.CommitteeRaiseLetterId == committeeRaiseLetterId);
       }
       public string EditCommitteeRaiseLetter(CommitteeRaiseLetter model)
       {
           CommitteeRaiseLetter committeeRaiseLetter = new CommitteeRaiseLetter();
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
                   _path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/CommitteeRaiseLetterDocument"), _TenderName);
                   tenderDocument.SaveAs(_path);
                   _tenderUrl = "~/CommitteeRaiseLetterDocument/" + "" + _TenderName;
                   long committeeRaiseLetterId = Convert.ToInt64(model.CommitteeRaiseLetterId);
                   committeeRaiseLetter = _iCommitteeRaiseLetterRepository.FindOne(x => x.CommitteeRaiseLetterId == committeeRaiseLetterId);
                   committeeRaiseLetter.FileNo = model.FileNo;
                   committeeRaiseLetter.CommonCodeTypeValueId = model.CommonCodeTypeValueId;
                   committeeRaiseLetter.IssueDate = model.IssueDate;
                   committeeRaiseLetter.AttachedDocument = _tenderUrl;
                   committeeRaiseLetter.FromAuthority = model.FromAuthority;
                   committeeRaiseLetter.ToAuthority = model.ToAuthority;
                   committeeRaiseLetter.InformedAuthority = model.InformedAuthority;
                   committeeRaiseLetter.UpdatedBy = PortalContext.CurrentUser.UserName;
                   committeeRaiseLetter.UpdatedDate = DateTime.Now;
                   int result = _iCommitteeRaiseLetterRepository.Edit(committeeRaiseLetter);
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
               long committeeRaiseLetterId = Convert.ToInt64(model.CommitteeRaiseLetterId);
               committeeRaiseLetter = _iCommitteeRaiseLetterRepository.FindOne(x => x.CommitteeRaiseLetterId == committeeRaiseLetterId);
               committeeRaiseLetter.FileNo = model.FileNo;
               committeeRaiseLetter.CommonCodeTypeValueId = model.CommonCodeTypeValueId;
               committeeRaiseLetter.IssueDate = model.IssueDate;
               committeeRaiseLetter.FromAuthority = model.FromAuthority;
               committeeRaiseLetter.ToAuthority = model.ToAuthority;
               committeeRaiseLetter.InformedAuthority = model.InformedAuthority;
               committeeRaiseLetter.UpdatedBy = PortalContext.CurrentUser.UserName;
               committeeRaiseLetter.UpdatedDate = DateTime.Now;
               int result = _iCommitteeRaiseLetterRepository.Edit(committeeRaiseLetter);
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

        public CommitteeRaiseLetter GetCommitteeRaiseLetterByFile(string fileId)
        {
            long identity = Convert.ToInt64(fileId);
            return _iCommitteeRaiseLetterRepository.FindOne(x => x.FileNo == identity);
        }

        public List<CommitteeRaiseLetter> GetCommitteeRaiseLettersbyProId(string proId)
        {
            long ss = Convert.ToInt64(proId);
            return _iCommitteeRaiseLetterRepository.Filter(x => x.FileNo == ss).Include(x =>x.Demand).ToList();

        }
    }
}
