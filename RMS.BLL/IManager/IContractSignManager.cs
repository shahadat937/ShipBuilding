using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IContractSignManager
    {
        List<ContractSign> GetAllbyId(long? proId);
        ContractSign GetById(long? id);
        int Create(ContractSign modelContractSign);
      
    }
}
