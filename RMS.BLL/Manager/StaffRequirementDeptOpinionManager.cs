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
   public class StaffRequirementDeptOpinionManager : IStaffRequirementDeptOpinionManager
    {
          private readonly IStaffRequirementDeptOpinionRepository _staffRequirementDeptOpinionRepository;

          public StaffRequirementDeptOpinionManager(IStaffRequirementDeptOpinionRepository staffRequirementDeptOpinionRepository)
        {
            _staffRequirementDeptOpinionRepository = staffRequirementDeptOpinionRepository;
        }

       public List<StaffRequirementDeptOpinion> GetTenderSpecDeptOpinionByFileNo(long fileNo)
       {
           return _staffRequirementDeptOpinionRepository.Filter(x => x.FileNo == fileNo).ToList();
       }

       public StaffRequirementDeptOpinion SaveStaffRequirementDeptOpinion(StaffRequirementDeptOpinion tenderSpecDeptOpinion)
       {
           return _staffRequirementDeptOpinionRepository.Add(tenderSpecDeptOpinion);
       }

       public void DeleteTenderSpecDeptOpinion(long deptId, TenderSpecOpinion tenderSpecOpinion)
       {
           _staffRequirementDeptOpinionRepository.Delete(x => x.OpinionGivenDeptId == deptId && x.FileNo == tenderSpecOpinion.FileNo);

       }

       public void DeleteAllDept(long fileNo)
       {
           _staffRequirementDeptOpinionRepository.Delete(x => x.FileNo == fileNo);
       }

       public List<StaffRequirementDeptOpinion> GetstaffRequirementDeptOpinions(long? fileId)
       {
           return _staffRequirementDeptOpinionRepository.Filter(x => x.FileNo == fileId).Include(x => x.Department).Include(x => x.Demand).ToList();

       }

       public List<StaffRequirementDeptOpinion> GetDepartmentListByTenderName(string tendername, string fileNo)
       {
           long id = Convert.ToInt64(fileNo);
           //return _staffRequirementDeptOpinionRepository.Filter(x => x.TenderName == tendername & x.FileNo == id & x.IsComplete != true).ToList();
           return _staffRequirementDeptOpinionRepository.Filter(x => x.TenderName == tendername & x.FileNo == id).ToList();
       }

       public StaffRequirementDeptOpinion GetOpinionByProjectAndDept(long? projectID, long? depId)
       {
           return
               _staffRequirementDeptOpinionRepository.FindOne(
                   x => x.FileNo == projectID && x.OpinionGivenDeptId == depId);
       }
    }
}
