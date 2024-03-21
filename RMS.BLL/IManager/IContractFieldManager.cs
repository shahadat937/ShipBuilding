using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IContractFieldManager
    {
        string SaveContractField(ContractField contractField);

        List<ContractField> GetAllContractFields();

        string GetContractFieldNameById(long fieldId);

        ContractField GetContractFieldById(long contractFieldId);

        int DeleteContractField(ContractField contractField);

        string SaveContractFields(List<ContractField> models);
        List<ContractField> GetAllContractFieldByDemandId(long demandID);

        List<ContractField> GetAllContractFieldsByDemandId(long demandID);

        void DeleteContractFieldByDemandId(long p);
    }
}
