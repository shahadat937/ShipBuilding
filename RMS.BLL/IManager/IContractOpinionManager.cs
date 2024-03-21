using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IContractOpinionManager
    {
       ContractOpinion GetExistDataByIdentity(long opinionId);
       string SaveContractOpinion(ContractOpinion olddataaa);
       string SaveTenderSpecOpinion(ContractOpinion contractField);
       ContractOpinion GetContractOpinionById(long fileId);
       List<ContractOpinion> GetContractOpinionListByFileNo(string tenderSpecFileNo);
    }
}
