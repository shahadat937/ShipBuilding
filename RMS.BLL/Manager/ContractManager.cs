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
    public class ContractManager : IContractManager
    {
        private readonly IContractRepository _iContractRepository;
        public ContractManager(IContractRepository iContractRepository)
        {
            _iContractRepository = iContractRepository;
        }
        public string SaveContract(Contract model)
        {
            Contract contractt = _iContractRepository.FindOne(x => x.ContractId == model.ContractId);
            if (contractt == null)
            {
                Contract contract = new Contract();
                contract.ContractFileId = model.ContractFileId;
                contract.FieldId = model.FieldId;
                contract.FieldValue = model.FieldValue;
                contract.CreatedBy = PortalContext.CurrentUser.UserName;
                contract.CreatedDate = DateTime.Now;
                contract.UpdatedBy = PortalContext.CurrentUser.UserName;
                contract.UpdatedDate = DateTime.Now;
                int result = _iContractRepository.Save(contract);
                if (result > 0)
                {
                    return "Saved";
                }
                else
                {
                    return "Not Saved";
                }
            }
            else
            {
                Contract contract = new Contract();
                contract.ContractFileId = model.ContractFileId;
                contract.FieldId = model.FieldId;
                contract.FieldValue = model.FieldValue;
                contract.UpdatedBy = PortalContext.CurrentUser.UserName;
                contract.UpdatedDate = DateTime.Now;
                int result = _iContractRepository.Save(contract);
                if (result > 0)
                {
                    return "Saved";
                }
                else
                {
                    return "Not Saved";
                }
            }
        }
        public Contract GetContractById(long contId)
        {
            return _iContractRepository.FindOne(x => x.ContractId == contId);
        }


        public List<Contract> GetAllContracts()
        {
            return _iContractRepository.All().ToList();
        }
    }
}
