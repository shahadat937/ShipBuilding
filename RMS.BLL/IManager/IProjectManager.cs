using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IProjectManager
    {
       int Crate(Project project);
       List<Project> GetCurrentProjects();
       List<Project> GetNewlyProjects();
       List<Project> GetAllProjects();
       Project GetProjectByIdentity(long? Id);

       spProjectTaskStatus GetProjectStatus(string projectId);
       List<spProjectOpinion_Result> GetAllOpinion(string projectId);
       List<TenderSpecificationChild> GetTenderSpecChild(string projectId);
    }
}
