using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
   public class ContractOpinionManager : IContractOpinionManager
    {
             private readonly IContractOpinionRepository _contractOpinionRepository;
             public ContractOpinionManager(IContractOpinionRepository contractOpinionRepository)
        {
            _contractOpinionRepository = contractOpinionRepository;
        }

       public ContractOpinion GetExistDataByIdentity(long opinionId)
       {
           return _contractOpinionRepository.FindOne(x => x.OpinionId == opinionId);
       }

       public string SaveContractOpinion(ContractOpinion olddataaa)
       {
           olddataaa.CreatedDate = DateTime.Now.Date;
           olddataaa.CreatedBy = PortalContext.CurrentUser.UserName;
           int result = _contractOpinionRepository.Save(olddataaa);
           if (result > 0)
           {
               olddataaa.Message = "Successfully Edited document";
           }
           else
           {
               olddataaa.Message = "Edit failed";
           }

           return olddataaa.Message;
       }

       public string SaveTenderSpecOpinion(ContractOpinion contractField)
       {
           contractField.CreatedDate = DateTime.Now.Date;
           contractField.CreatedBy = PortalContext.CurrentUser.UserName;
           int result = _contractOpinionRepository.Save(contractField);
           if (result > 0)
           {
               contractField.Message = "Successfully Edited document";
           }
           else
           {
               contractField.Message = "Edit failed";
           }

           return contractField.Message;
       }

       public ContractOpinion GetContractOpinionById(long fileId)
       {
           return _contractOpinionRepository.FindOne(x => x.FileNo == fileId);
       }

       public List<ContractOpinion> GetContractOpinionListByFileNo(string tenderSpecFileNo)
       {
           long id = Convert.ToInt64(tenderSpecFileNo);
           return _contractOpinionRepository.Filter(x => x.FileNo == id).Include(x =>x.Demand).ToList();
       }
    }
}
