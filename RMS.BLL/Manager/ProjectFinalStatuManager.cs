using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
   public class ProjectFinalStatuManager :IProjectFinalStatuManager
   {
       private IProjectFinalStatuRepository _projectFinalStatuRepository;

       public ProjectFinalStatuManager(IProjectFinalStatuRepository projectFinalStatuRepository)
       {
           _projectFinalStatuRepository = projectFinalStatuRepository;
       }

       public List<ProjectFinalStatu> GetAll()
       {
           return _projectFinalStatuRepository.All().ToList();
       }
   }
}
