using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class ProjectManager:IProjectManager
    {
        private readonly IProjectRepository _iRepository;
        private readonly ITenderSpecificationChildRepository _tenderSpecificationChildRepository;
        public ProjectManager(IProjectRepository iRepository )
        {
            _iRepository = iRepository;
            _tenderSpecificationChildRepository = new TenderSpecificationChildRepository();
        }
        public int Crate(Project project)
        {
            return _iRepository.Save(project);
        }
        public List<Project> GetAllProjects()
        {
            return _iRepository.All().ToList();
        }
        public List<Project> GetCurrentProjects()
        {
            return _iRepository.Filter(x => x.Status == 1).ToList();
        }
        public List<Project> GetNewlyProjects()
        {
            return _iRepository.Filter(x => x.Status == 2).ToList();
        }
        public Project GetProjectByIdentity(long? Id)
        {
            return _iRepository.FindOne(x => x.ProjectIdentity == Id);
        }

        public spProjectTaskStatus GetProjectStatus(string projectId)
        {
            return _iRepository.GetProjectStatus(projectId);
        }

        public List<spProjectOpinion_Result> GetAllOpinion(string projectId)
        {
            return _iRepository.GetAllOpinion(projectId);
        }

        public List<TenderSpecificationChild> GetTenderSpecChild(string projectId)
        {
            long? pId = projectId != null ? Convert.ToInt64(projectId) : 0;
            return _tenderSpecificationChildRepository.Filter(x => x.ProjectId == pId && x.IsChecked).ToList();
        }
    }
}
