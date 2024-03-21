using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly DSBDBEntities _context;
        public ProjectRepository()
            : base()
        {
            _context = base.Context;
        }


        public spProjectTaskStatus GetProjectStatus(string projectId)
        {
            return Context.Database.SqlQuery<spProjectTaskStatus>("exec dbo.spProjectTaskStatus " + projectId + "").FirstOrDefault();
             
        }

        public List<spProjectOpinion_Result> GetAllOpinion(string projectId)
        {
            return Context.spProjectOpinion(projectId).ToList();
        }
    }
}
