//using System.Data.Entity;
//using RMS.BLL.IManager;
//using RMS.DAL.IRepository;
//using RMS.Model;
//using RMS.Utility;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMS.BLL.Manager
//{
//    public class TenderSpecOpinionManager : BaseManager, ITenderSpecOpinionManager
//    {
//        private readonly ITenderSpecOpinionRepository _tenderSpecOpinionRepository;
//        private readonly ITenderSpecDeptOpinionRepository _tenderSpecDeptOpinionRepository;
//        public TenderSpecOpinionManager(ITenderSpecOpinionRepository tenderSpecOpinionRepository, ITenderSpecDeptOpinionRepository tenderSpecDeptOpinionRepository)
//        {
//            _tenderSpecOpinionRepository = tenderSpecOpinionRepository;
//            _tenderSpecDeptOpinionRepository = tenderSpecDeptOpinionRepository;
//        }
//        public string SaveTenderSpecOpinion(TenderSpecOpinion model)
//        {
//            model.CreatedDate = DateTime.Now.Date;
//            model.CreatedBy = PortalContext.CurrentUser.UserName;
//                        int result = _tenderSpecOpinionRepository.Save(model);
//                        if (result > 0)
//                        {
//                            model.Message = "Successfully Edited document";
//                        }
//                        else
//                        {
//                            model.Message = "Edit failed";
//                        }
  
//                return model.Message;
           
//        }
//        public TenderSpecOpinion GetTenderSpecOpinionDocumentByTSId(int tenderSpecOpinionId)
//        {

//            return _tenderSpecOpinionRepository.FindOne(x => x.OpinionId == tenderSpecOpinionId);
//        }
//        public List<TenderSpecOpinion> GetAllTenderSpecifications()
//        {
//            return _tenderSpecOpinionRepository.All().ToList();
//        }
//        public TenderSpecOpinion GetTenderSpecOpinionById(long? tenderSpecificationById)
//        {
//            return _tenderSpecOpinionRepository.FindOne(x => x.FileNo == tenderSpecificationById);
//        }
//        public int Delete(TenderSpecOpinion tenderSpecification)
//        {
//            int result = _tenderSpecOpinionRepository.Delete(tenderSpecification);
//            return result;
//        }

//        public List<TenderSpecOpinion> GetTenderSpecificationsBySearchKey(string searchKey)
//        {
//            return _tenderSpecOpinionRepository.Filter(x =>x.TenderName.ToLower().Contains(searchKey.ToLower()) ||
//                x.TenderUrl.ToLower().Contains(searchKey.ToLower())).ToList();
//        }
//        public string GetTenderUrl(string fileNo, string tenderName)
//        {
//            return _tenderSpecOpinionRepository.FindOne(x => x.FileNo == Convert.ToInt64(fileNo) && x.TenderName == tenderName).TenderUrl;
//        }
//        public DateTime? GetTenderSpecOpinionManagerIssueDate(string fileNo, string tenderName)
//        {
//            return _tenderSpecOpinionRepository.FindOne(x => x.FileNo == Convert.ToInt64(fileNo) && x.TenderName == tenderName).IssueDate;
//        }

//        public List<TenderSpecOpinion> GetTenderListByFileNo(string tenderSpecFileNo)
//        {
//            long id = Convert.ToInt64(tenderSpecFileNo);
//            return _tenderSpecOpinionRepository.Filter(x => x.FileNo == id).ToList();
//        }

//        public List<TenderSpecOpinion> GetDepartmentListByTenderName(string tenderName)
//        {
//            return _tenderSpecOpinionRepository.Filter(x => x.TenderName == tenderName).ToList();
//        }

//        public TenderSpecOpinion GetExistDataByIdentity(long tenderSpecOpinionId)
//        {
//            return _tenderSpecOpinionRepository.FindOne(x => x.OpinionId == tenderSpecOpinionId);
//        }

//        public List<TenderSpecDeptOpinion> GetTenderDeptInfoByProject(long? proId)
//        {
//            return _tenderSpecDeptOpinionRepository.Filter(x => x.FileNo == proId).ToList();
//        }
//    }
//}



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
    public class TenderSpecOpinionManager : BaseManager, ITenderSpecOpinionManager
    {
        private readonly ITenderSpecOpinionRepository _tenderSpecOpinionRepository;
        private readonly ITenderSpecDeptOpinionRepository _tenderSpecDeptOpinionRepository;
        private readonly IDemandManager _iDemandManager;
        public TenderSpecOpinionManager(ITenderSpecOpinionRepository tenderSpecOpinionRepository, ITenderSpecDeptOpinionRepository tenderSpecDeptOpinionRepository,
            IDemandManager iDemandManager)
        {
            _tenderSpecOpinionRepository = tenderSpecOpinionRepository;
            _tenderSpecDeptOpinionRepository = tenderSpecDeptOpinionRepository;
            _iDemandManager = iDemandManager;
        }
        public string SaveTenderSpecOpinion(TenderSpecOpinion model)
        {
            model.CreatedDate = DateTime.Now.Date;
            model.CreatedBy = PortalContext.CurrentUser.UserName;
            int result = _tenderSpecOpinionRepository.Save(model);
            if (result > 0)
            {
                model.Message = "Successfully Edited document";
            }
            else
            {
                model.Message = "Edit failed";
            }

            return model.Message;

        }
        public TenderSpecOpinion GetTenderSpecOpinionDocumentByTSId(int tenderSpecOpinionId)
        {

            return _tenderSpecOpinionRepository.FindOne(x => x.OpinionId == tenderSpecOpinionId);
        }
        public List<TenderSpecOpinion> GetAllTenderSpecifications()
        {
            List<TenderSpecOpinion> TenderSpecOpinionList = new List<TenderSpecOpinion>();
            List<TenderSpecOpinion> tenderSpecOpinions = _tenderSpecOpinionRepository.All().ToList();
            foreach (var item in tenderSpecOpinions)
            {
                item.ProjectName = _iDemandManager.GetProjectNameById(item.FileNo);

                TenderSpecOpinionList.Add(item);
            }
            return TenderSpecOpinionList;
        }
        public TenderSpecOpinion GetTenderSpecOpinionById(long? tenderSpecificationById)
        {
            return _tenderSpecOpinionRepository.FindOne(x => x.FileNo == tenderSpecificationById);
        }
        public int Delete(TenderSpecOpinion tenderSpecification)
        {
            int result = _tenderSpecOpinionRepository.Delete(tenderSpecification);
            return result;
        }

        public List<TenderSpecOpinion> GetTenderSpecificationsBySearchKey(string searchKey)
        {
            return _tenderSpecOpinionRepository.Filter(x => x.TenderName.ToLower().Contains(searchKey.ToLower()) ||
                x.TenderUrl.ToLower().Contains(searchKey.ToLower())).ToList();
        }
        public string GetTenderUrl(string fileNo, string tenderName)
        {
            return _tenderSpecOpinionRepository.FindOne(x => x.FileNo == Convert.ToInt64(fileNo) && x.TenderName == tenderName).TenderUrl;
        }
        public DateTime? GetTenderSpecOpinionManagerIssueDate(string fileNo, string tenderName)
        {
            return _tenderSpecOpinionRepository.FindOne(x => x.FileNo == Convert.ToInt64(fileNo) && x.TenderName == tenderName).IssueDate;
        }

        public List<TenderSpecOpinion> GetTenderListByFileNo(string tenderSpecFileNo)
        {
            long id = Convert.ToInt64(tenderSpecFileNo);
            return _tenderSpecOpinionRepository.Filter(x => x.FileNo == id).ToList();
        }

        public List<TenderSpecOpinion> GetDepartmentListByTenderName(string tenderName)
        {
            return _tenderSpecOpinionRepository.Filter(x => x.TenderName == tenderName).ToList();
        }

        public TenderSpecOpinion GetExistDataByIdentity(long tenderSpecOpinionId)
        {
            return _tenderSpecOpinionRepository.FindOne(x => x.OpinionId == tenderSpecOpinionId);
        }

        public List<TenderSpecOpinion> GetTenderDeptInfoByProject(long? proId)
        {
            return _tenderSpecOpinionRepository.Filter(x => x.FileNo == proId).Include(x =>x.Demand).ToList();
        }


        public List<TenderSpecOpinion> GetAllTenderSpecificationBySearchKey(string searchKey)
        {
            List<TenderSpecOpinion> TenderSpecOpinions = new List<TenderSpecOpinion>();
            List<TenderSpecOpinion> tenderSpecOpinions = _tenderSpecOpinionRepository.Filter(x => x.TenderName.ToLower().Contains(searchKey.ToLower())).ToList();
            if (tenderSpecOpinions.Count > 0)
            {
                foreach (var item in tenderSpecOpinions)
                {
                    item.ProjectName = _iDemandManager.GetProjectNameById(item.FileNo);
                    TenderSpecOpinions.Add(item);
                }
            }
            return TenderSpecOpinions;
        }
    }
}
