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
    public class ContractFileManager : IContractFileManager
    {
        private readonly IContractFileRepository _iContractFileRepository;
        public ContractFileManager(IContractFileRepository iContractFileRepository)
        {
            _iContractFileRepository = iContractFileRepository;
        }
        public List<ContractFile> GetAllContractFiles()
        {
            return _iContractFileRepository.All().ToList();
        }

        public List<ContractFile> GetContractFilesBySearchKey(string searchKey)
        {
            return _iContractFileRepository.Filter(x =>
                x.FileName.ToLower().Contains(searchKey.ToLower()) ||
                x.FileUrl.ToLower().Contains(searchKey.ToLower())).ToList();
        }

        public string SaveContractFile(ContractFile model)
        {
            int result = _iContractFileRepository.Save(model);
            if (result > 0)
            {
                return "Contract File has been created successfully!";
            }
            else
            {
                return "Contract File has not been created";
            }
        }
        public ContractFile GetContractFileById(long contractFileId)
        {
            return _iContractFileRepository.FindOne(x => x.ContractFileId == contractFileId);
        }
        public ContractFile GetContractFileByDemandId(long demandId)
        {
            return _iContractFileRepository.FindOne(x => x.DemandId == demandId);
        }

        public string GetCompleteProject()
        {
            var ss = _iContractFileRepository.Filter(x => x.IsApproved.Equals(true)).Where(x => x.Demand.ProjectType == 6).GroupBy(x=>x.DemandId).Count();
            return ss.ToString();
        }

        public string GetOnGoingProject()
        {
            var ss = _iContractFileRepository.Filter(x => x.IsApproved.Equals(false)).Where(x => x.Demand.ProjectType == 6).GroupBy(x => x.DemandId).Count();
            return ss.ToString();
        }

        public int DeleteContractFile(ContractFile contractFile)
        {
            return _iContractFileRepository.Delete(contractFile);
        }
        public long? GetContractFileIdByName(string fileName)
        {
            return _iContractFileRepository.FindOne(x => x.FileName == fileName).ContractFileId;
        }
        public string GetContractFile(long contractFileId)
        {
            return _iContractFileRepository.FindOne(x => x.ContractFileId == contractFileId).FileName;
        }
       
    }
}
