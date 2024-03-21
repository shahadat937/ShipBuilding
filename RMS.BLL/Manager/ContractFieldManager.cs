using System.Data.Entity;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.Manager
{
    public class ContractFieldManager : IContractFieldManager
    {
        private readonly IContractFieldRepository _iContractFieldRepository;
        public ContractFieldManager(IContractFieldRepository iContractFieldRepository)
        {
            _iContractFieldRepository = iContractFieldRepository;
        }
        public string SaveContractField(ContractField model)
        {
            ContractField contField = _iContractFieldRepository.FindOne(x => x.FieldId == model.FieldId);
            if (contField == null)
            {
                return "No Contract Field has been found with this data";
            }
            else
            {
                contField.FieldName = model.FieldName;
                contField.FieldValue = model.FieldValue;
                contField.ContractFileId = model.ContractFileId;
                contField.UpdatedBy = PortalContext.CurrentUser.UserName;
                contField.UpdatedDate = DateTime.Now;
                int result = _iContractFieldRepository.Save(contField);
                if (result > 0)
                {
                    return "Contract Field has been updated";
                }
                else
                {
                    return "Error occured";
                }
            }
        }
        public List<ContractField> GetAllContractFields()
        {
            return _iContractFieldRepository.All().Include(x =>x.CommonCode).ToList();
        }


        public string GetContractFieldNameById(long fieldId)
        {
            return _iContractFieldRepository.FindOne(x => x.FieldId == fieldId).CommonCode.TypeValue;
        }


        public ContractField GetContractFieldById(long contractFieldId)
        {
            return _iContractFieldRepository.FindOne(x => x.FieldId == contractFieldId);
        }
        public int DeleteContractField(ContractField contractField)
        {
            return _iContractFieldRepository.Delete(x => x.FieldId == contractField.FieldId);
        }
        public string SaveContractFields(List<ContractField> models)
        {
            if (models == null)
            {
                return "There is no article , please enter article";
            }

            foreach (var item in models)
            {
                ContractField contractField = new ContractField();
                contractField.FieldName = item.FieldName;
                contractField.FieldValue = item.FieldValue;
                contractField.ContractFileId = item.ContractFileId;
                contractField.CreatedBy = PortalContext.CurrentUser.UserName;
                contractField.CreatedDate = DateTime.Now;
                contractField.UpdatedBy = PortalContext.CurrentUser.UserName;
                contractField.UpdatedDate = DateTime.Now;
                _iContractFieldRepository.Save(contractField);
            }
            return "Data have been saved";
        }


        public List<ContractField> GetAllContractFieldByDemandId(long demandId)
        {
            return _iContractFieldRepository.Filter(x => x.ContractFileId == demandId).ToList();
        }
        public List<ContractField> GetAllContractFieldsByDemandId(long demandID)
        {
            return _iContractFieldRepository.Filter(x => x.ContractFileId == demandID).ToList();
        }


        public void DeleteContractFieldByDemandId(long p)
        {
            _iContractFieldRepository.Delete(x => x.ContractFileId == p);
        }
    }
}
