//using RMS.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace RMS.BLL.IManager
//{
//    public interface ITenderSpecOpinionManager
//    {
//        List<TenderSpecOpinion> GetAllTenderSpecifications();

//        List<TenderSpecOpinion> GetTenderSpecificationsBySearchKey(string searchKey);


//        string SaveTenderSpecOpinion(TenderSpecOpinion tenderSpecOpinion);
//        TenderSpecOpinion GetTenderSpecOpinionDocumentByTSId(int tenderSpecOpinionId);


//        TenderSpecOpinion GetTenderSpecOpinionById(long? tenderSpecificationById);

//        int Delete(TenderSpecOpinion tenderSpecOpinion);

      

//        string GetTenderUrl(string fileNo, string tenderName);

//        DateTime? GetTenderSpecOpinionManagerIssueDate(string fileNo, string tenderName);
//        List<TenderSpecOpinion> GetTenderListByFileNo(string tenderSpecFileNo);
//        List<TenderSpecOpinion> GetDepartmentListByTenderName(string tenderName);
//        TenderSpecOpinion GetExistDataByIdentity(long tenderSpecOpinionId);

//        List<TenderSpecDeptOpinion> GetTenderDeptInfoByProject(long? proId);
//    }
//}

using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface ITenderSpecOpinionManager
    {
        List<TenderSpecOpinion> GetAllTenderSpecifications();

        List<TenderSpecOpinion> GetTenderSpecificationsBySearchKey(string searchKey);


        string SaveTenderSpecOpinion(TenderSpecOpinion tenderSpecOpinion);
        TenderSpecOpinion GetTenderSpecOpinionDocumentByTSId(int tenderSpecOpinionId);


        TenderSpecOpinion GetTenderSpecOpinionById(long? tenderSpecificationById);

        int Delete(TenderSpecOpinion tenderSpecOpinion);



        string GetTenderUrl(string fileNo, string tenderName);

        DateTime? GetTenderSpecOpinionManagerIssueDate(string fileNo, string tenderName);
        List<TenderSpecOpinion> GetTenderListByFileNo(string tenderSpecFileNo);
        List<TenderSpecOpinion> GetDepartmentListByTenderName(string tenderName);
        TenderSpecOpinion GetExistDataByIdentity(long tenderSpecOpinionId);

        List<TenderSpecOpinion> GetTenderDeptInfoByProject(long? proId);

        List<TenderSpecOpinion> GetAllTenderSpecificationBySearchKey(string searchKey);
    }
}

