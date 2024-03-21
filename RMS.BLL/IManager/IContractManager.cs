using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IContractManager
    {
        string SaveContract(Contract contract);

        Contract GetContractById(long contId);

        List<Contract> GetAllContracts();
    }
}
