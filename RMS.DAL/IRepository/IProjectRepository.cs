using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.DAL.IRepository
{
    public interface IProjectRepository : IRepository<Project>
    {
        spProjectTaskStatus GetProjectStatus(string projectId);
        List<spProjectOpinion_Result> GetAllOpinion(string projectId);
    }
}
