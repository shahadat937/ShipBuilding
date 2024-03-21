using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
  public  interface IContractDeptOpinionManager
    {
      ContractDeptOpinion SaveContractDeptOpinion(ContractDeptOpinion tenderSpecDeptOpinion);
      List<ContractDeptOpinion> GetTenderSpecDeptOpinionByFileNo(long fileNo);
      void DeleteAllDept(long fileNo);
      List<ContractDeptOpinion> GetContractDeptOpinions(long? fileId);
      List<ContractDeptOpinion> GetDepartmentListByTenderName(string tendernameee, string fileNo);
      ContractDeptOpinion GetOpinionByProjectAndDept(long? fileNo, long? depId);
    }
}
