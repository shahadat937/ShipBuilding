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
  public class StaffRequirementOpinionManager : IStaffRequirementOpinionManager
    {
             private readonly IStaffRequirementOpinionRepository _staffRequirementOpinionRepository;
             public StaffRequirementOpinionManager(IStaffRequirementOpinionRepository staffRequirementOpinionRepository)
        {
            _staffRequirementOpinionRepository = staffRequirementOpinionRepository;
        }

      public StaffRequirementOpinion GetstaffRequirementById(long fileId)
      {
          return _staffRequirementOpinionRepository.FindOne(x => x.FileNo == fileId);

      }

      public StaffRequirementOpinion GetExistDataByIdentity(long opinionId)
      {
          return _staffRequirementOpinionRepository.FindOne(x => x.OpinionId == opinionId);
      }

      public string SaveTenderSpecOpinion(StaffRequirementOpinion olddataa)
      {
          olddataa.CreatedDate = DateTime.Now.Date;
          olddataa.CreatedBy = PortalContext.CurrentUser.UserName;
          int result = _staffRequirementOpinionRepository.Save(olddataa);
          if (result > 0)
          {
              olddataa.Message = "Successfully Edited document";
          }
          else
          {
              olddataa.Message = "Edit failed";
          }

          return olddataa.Message;
      }

      public List<StaffRequirementOpinion> GetStaffRequireListByFileNo(string tenderSpecFileNo)
      {
          long id = Convert.ToInt64(tenderSpecFileNo);
          return _staffRequirementOpinionRepository.Filter(x => x.FileNo == id).Include(x =>x.Demand).ToList();
      }

  
    }
}
