using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IStaffRequirementOpinionManager
    {
       StaffRequirementOpinion GetstaffRequirementById(long fileId);
       StaffRequirementOpinion GetExistDataByIdentity(long opinionId);

       string SaveTenderSpecOpinion(StaffRequirementOpinion olddataa);
       List<StaffRequirementOpinion> GetStaffRequireListByFileNo(string tenderSpecFileNo);
    }
}
