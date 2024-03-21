using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface ITenderSpecDeptOpinionManager
    {
        TenderSpecDeptOpinion SaveTenderSpecDeptOpinion(TenderSpecDeptOpinion tenderSpecDeptOpinion);

        List<TenderSpecDeptOpinion> GetTenderSpecDeptOpinions(long? fileId);

        List<TenderSpecDeptOpinion> GetTenderSpecificationsBySearchKey(string searchKey);

        string SaveTenderSpecDeptOpinionWithOpinionUrl(TenderSpecDeptOpinion tenderSpecDeptOpinion, string formId);

        TenderSpecDeptOpinion GetTenderSpecDeptOpinionById(long tenderSpecDeptOpinionId);

        int Delete(TenderSpecDeptOpinion tenderSpecDeptOpinion);

        List<TenderSpecDeptOpinion> GetTenderListByFileNo(string tenderSpecFileNo);

        List<TenderSpecDeptOpinion> GetDepartmentListByTenderName(string tenderName, string fileno);



        string EditTenderSpecDeptOpinionWithOpinionUrl(TenderSpecDeptOpinion tenderSpecDeptOpinion);
        TenderSpecDeptOpinion FindDataByFlowAndDepId(long fileNo, long opinionGivenDeptId);
        List<TenderSpecDeptOpinion> GetTenderSpecDeptOpinionByFileNo(long fileNo);
        void DeleteTenderSpecDeptOpinion(long deptId, TenderSpecOpinion tenderSpecOpinion);
        void DeleteAllDept(long fileNo);
        TenderSpecDeptOpinion GetOpinionByProjectAndDept(long? projectId, long? depId);
    }
}
