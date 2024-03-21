using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IStaffRequirementDeptOpinionManager
    {
       List<StaffRequirementDeptOpinion> GetTenderSpecDeptOpinionByFileNo(long fileNo);
       StaffRequirementDeptOpinion SaveStaffRequirementDeptOpinion(StaffRequirementDeptOpinion tenderSpecDeptOpinion);
       void DeleteTenderSpecDeptOpinion(long deptId, TenderSpecOpinion tenderSpecOpinion);

       void DeleteAllDept(long fileNo);
       List<StaffRequirementDeptOpinion> GetstaffRequirementDeptOpinions(long? fileId);
       List<StaffRequirementDeptOpinion> GetDepartmentListByTenderName(string tendername, string fileNo);
       StaffRequirementDeptOpinion GetOpinionByProjectAndDept(long? projectID, long? depId);
    }
}
