using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IContractFileManager
    {
        List<ContractFile> GetAllContractFiles();

        List<ContractFile> GetContractFilesBySearchKey(string searchKey);

        string SaveContractFile(ContractFile contractFile);

        ContractFile GetContractFileById(long contractFileId);

        int DeleteContractFile(ContractFile contractFile);

        long? GetContractFileIdByName(string fileName);

        string GetContractFile(long contractFileId);

        ContractFile GetContractFileByDemandId(long demandId);


        string GetCompleteProject();
        string GetOnGoingProject();
    }
}
