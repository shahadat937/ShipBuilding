using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
   public class ContractDeptOpinionManager :IContractDeptOpinionManager
    {
          private readonly IContractDeptOpinionRepository _iContractDeptOpinionRepository;
          public ContractDeptOpinionManager(IContractDeptOpinionRepository iContractDeptOpinionRepository)
        {
            _iContractDeptOpinionRepository = iContractDeptOpinionRepository;
        }

       public ContractDeptOpinion SaveContractDeptOpinion(ContractDeptOpinion tenderSpecDeptOpinion)
       {
           return _iContractDeptOpinionRepository.Add(tenderSpecDeptOpinion);
       }

       public List<ContractDeptOpinion> GetTenderSpecDeptOpinionByFileNo(long fileNo)
       {
           return _iContractDeptOpinionRepository.Filter(x => x.FileNo == fileNo).ToList();
       }

       public void DeleteAllDept(long fileNo)
       {
            _iContractDeptOpinionRepository.Delete(x => x.FileNo == fileNo);
       }

       public List<ContractDeptOpinion> GetContractDeptOpinions(long? fileId)
       {
           return _iContractDeptOpinionRepository.Filter(x => x.FileNo == fileId).Include(x => x.Department).Include(x => x.Demand).ToList();

       }

       public List<ContractDeptOpinion> GetDepartmentListByTenderName(string tendernameee, string fileNo)
       {
           long id = Convert.ToInt64(fileNo);
           //return _iContractDeptOpinionRepository.Filter(x => x.TenderName == tendernameee & x.FileNo == id & x.IsComplete != true).ToList();
           return _iContractDeptOpinionRepository.Filter(x => x.TenderName == tendernameee & x.FileNo == id ).ToList();
       }

       public ContractDeptOpinion GetOpinionByProjectAndDept(long? fileNo, long? depId)
       {
           return _iContractDeptOpinionRepository.FindOne(x => x.FileNo == fileNo && x.OpinionGivenDeptId == depId);
       }
    }
}
