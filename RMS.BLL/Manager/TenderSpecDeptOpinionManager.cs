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
    public class TenderSpecDeptOpinionManager : BaseManager, ITenderSpecDeptOpinionManager
    {
        private readonly ITenderSpecDeptOpinionRepository _iTenderSpecDeptOpinionRepository;
        private readonly IStaffRequirementDeptOpinionRepository _iStaffRequirementDeptOpinionRepository;
        private readonly IContractDeptOpinionRepository _contractDeptOpinionRepository;
        public TenderSpecDeptOpinionManager(ITenderSpecDeptOpinionRepository iTenderSpecDeptOpinionRepository, IStaffRequirementDeptOpinionRepository iStaffRequirementDeptOpinionRepository, IContractDeptOpinionRepository contractDeptOpinionRepository)
        {
            _iTenderSpecDeptOpinionRepository = iTenderSpecDeptOpinionRepository;
            _iStaffRequirementDeptOpinionRepository = iStaffRequirementDeptOpinionRepository;
            _contractDeptOpinionRepository = contractDeptOpinionRepository;
        }
        public TenderSpecDeptOpinion SaveTenderSpecDeptOpinion(TenderSpecDeptOpinion tenderSpecDeptOpinion)
        {
            return _iTenderSpecDeptOpinionRepository.Add(tenderSpecDeptOpinion);
        }
        public List<TenderSpecDeptOpinion> GetTenderSpecDeptOpinions(long? fileId)
        {
            return _iTenderSpecDeptOpinionRepository.Filter(x =>x.FileNo == fileId).Include(x =>x.Department).Include(x =>x.Demand).ToList();
        }
        public List<TenderSpecDeptOpinion> GetTenderSpecificationsBySearchKey(string searchKey)
        {
            return _iTenderSpecDeptOpinionRepository.Filter(x =>x.TenderName.ToLower().Contains(searchKey.ToLower())).ToList();
        }
        public string SaveTenderSpecDeptOpinionWithOpinionUrl(TenderSpecDeptOpinion model, string formId)
        {
            string _TenderName = "";
            string _path = "";
            string _tenderUrl =null;
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
                    _path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/TenderSpecDeptOpinionUploadedDocument"), _TenderName);
                    tenderDocument.SaveAs(_path);
                    _tenderUrl = _path;
                }
            }
            switch (formId)
            {
                case "10008":
                    var oldData = _iTenderSpecDeptOpinionRepository.FindOne(x => x.FileNo == model.FileNo && x.OpinionGivenDeptId == model.OpinionGivenDeptId);
                    if (oldData != null)
                    {
                        oldData.OpinionUrl = _tenderUrl ?? oldData.OpinionUrl;
                        oldData.Remarks = model.Remarks;
                        oldData.UpdatedBy = PortalContext.CurrentUser.UserName;
                        oldData.UpdatedDate = DateTime.Now;
                        oldData.IsComplete = true;
                    }
                    int result = _iTenderSpecDeptOpinionRepository.Save(oldData);
                    message = result > 0 ? "Successfully Saved document" : "Upload failed";
                    break;

                case "10013":
                    var oldDataa = _iStaffRequirementDeptOpinionRepository.FindOne(x => x.FileNo == model.FileNo && x.OpinionGivenDeptId == model.OpinionGivenDeptId);
                    if (oldDataa != null)
                    {
                        oldDataa.OpinionUrl = _tenderUrl ?? oldDataa.OpinionUrl;
                        oldDataa.Remarks = model.Remarks;
                        oldDataa.UpdatedBy = PortalContext.CurrentUser.UserName;
                        oldDataa.UpdatedDate = DateTime.Now;
                        oldDataa.IsComplete = true;
                    }
                    int resultt = _iStaffRequirementDeptOpinionRepository.Save(oldDataa);
                    message = resultt > 0 ? "Successfully Saved document" : "Upload failed";
                    break;

                case "10015":
                    var oldDaata = _contractDeptOpinionRepository.FindOne(x => x.FileNo == model.FileNo && x.OpinionGivenDeptId == model.OpinionGivenDeptId);
                    if (oldDaata != null)
                    {
                        oldDaata.OpinionUrl = _tenderUrl ?? oldDaata.OpinionUrl;
                        oldDaata.Remarks = model.Remarks;
                        oldDaata.UpdatedBy = PortalContext.CurrentUser.UserName;
                        oldDaata.UpdatedDate = DateTime.Now;
                        oldDaata.IsComplete = true;
                    }
                    int reslt = _contractDeptOpinionRepository.Save(oldDaata);
                    message = reslt > 0 ? "Successfully Saved document" : "Upload failed";
                    break;
            }

            return message;
        }
        public TenderSpecDeptOpinion GetTenderSpecDeptOpinionById(long tenderSpecOpinionGivenId)
        {
            return _iTenderSpecDeptOpinionRepository.FindOne(x => x.TenderSpecOpinionGivenId == tenderSpecOpinionGivenId);
        }
        public int Delete(TenderSpecDeptOpinion tenderSpecDeptOpinion)
        {
            return _iTenderSpecDeptOpinionRepository.Delete(tenderSpecDeptOpinion);
        }
        public List<TenderSpecDeptOpinion> GetTenderListByFileNo(string tenderSpecFileNo)
        {
            long id = Convert.ToInt64(tenderSpecFileNo);
            return _iTenderSpecDeptOpinionRepository.Filter(x => x.FileNo == id).ToList();
        }
        public List<TenderSpecDeptOpinion> GetDepartmentListByTenderName(string name, string fileno)
        {
            long id = Convert.ToInt64(fileno);
            //return _iTenderSpecDeptOpinionRepository.Filter(x => x.TenderName == name & x.FileNo == id & x.IsComplete != true).ToList();
            return _iTenderSpecDeptOpinionRepository.Filter(x => x.TenderName == name & x.FileNo == id ).ToList();

        }
        public string EditTenderSpecDeptOpinionWithOpinionUrl(TenderSpecDeptOpinion model)
        {
            TenderSpecDeptOpinion tenderSpecDeptOpinion = new TenderSpecDeptOpinion();
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
                    _path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/TenderSpecDeptOpinionUploadedDocument"), _TenderName);
                    tenderDocument.SaveAs(_path);
                    _tenderUrl = "~/TenderSpecDeptOpinionUploadedDocument/" + "" + _TenderName;
                    long tenderSpecDeptOpinionId = Convert.ToInt64(model.TenderSpecOpinionGivenId);
                    tenderSpecDeptOpinion = _iTenderSpecDeptOpinionRepository.FindOne(x=> x.TenderSpecOpinionGivenId == model.TenderSpecOpinionGivenId);
                    if (tenderSpecDeptOpinion != null)
                    {
                        tenderSpecDeptOpinion.FileNo = model.FileNo;
                        tenderSpecDeptOpinion.TenderName = model.TenderName;
                        tenderSpecDeptOpinion.OpinionGivenDeptId = model.OpinionGivenDeptId;
                        tenderSpecDeptOpinion.OpinionUrl = _tenderUrl;
                        tenderSpecDeptOpinion.UpdatedBy = PortalContext.CurrentUser.UserName;
                        tenderSpecDeptOpinion.UpdatedDate = DateTime.Now;
                    }
                    int result = _iTenderSpecDeptOpinionRepository.Save(tenderSpecDeptOpinion);
                    if (result > 0)
                    {
                        message = "Successfully Saved document";
                    }
                    else
                    {
                        message = "Upload failed";
                    }
                }
            }
            else
            {
                tenderSpecDeptOpinion = _iTenderSpecDeptOpinionRepository.FindOne(x => x.TenderSpecOpinionGivenId==model.TenderSpecOpinionGivenId);
                tenderSpecDeptOpinion.FileNo = model.FileNo;
                tenderSpecDeptOpinion.TenderName = model.TenderName;
                tenderSpecDeptOpinion.OpinionGivenDeptId = model.OpinionGivenDeptId;
                tenderSpecDeptOpinion.UpdatedBy = PortalContext.CurrentUser.UserName;
                tenderSpecDeptOpinion.UpdatedDate = DateTime.Now;
                int result = _iTenderSpecDeptOpinionRepository.Edit(tenderSpecDeptOpinion);
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

        public TenderSpecDeptOpinion FindDataByFlowAndDepId(long fileNo, long opinionGivenDeptId)
        {
            return
                _iTenderSpecDeptOpinionRepository.FindOne(
                    x => x.FileNo == fileNo && x.OpinionGivenDeptId == opinionGivenDeptId);
        }

        public List<TenderSpecDeptOpinion> GetTenderSpecDeptOpinionByFileNo(long fileNo)
        {
            return _iTenderSpecDeptOpinionRepository.Filter(x => x.FileNo == fileNo).ToList();
 
        }

        public void DeleteTenderSpecDeptOpinion(long deptId, TenderSpecOpinion tenderSpecOpinion)
        {
            _iTenderSpecDeptOpinionRepository.Delete(x => x.OpinionGivenDeptId == deptId && x.FileNo == tenderSpecOpinion.FileNo);
        }

        public void DeleteAllDept(long fileNo)
        {
            _iTenderSpecDeptOpinionRepository.Delete(x => x.FileNo == fileNo);
        }

        public TenderSpecDeptOpinion GetOpinionByProjectAndDept(long? projectId, long? depId)
        {
            return _iTenderSpecDeptOpinionRepository.FindOne(x => x.FileNo == projectId && x.OpinionGivenDeptId == depId);
        }
    }
}
