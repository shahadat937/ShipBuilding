using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class ProjectTypeManager:IProjectTypeManager
    {
        private readonly IProjectTypeRepository _iProjectTypeRepository;
        public ProjectTypeManager(IProjectTypeRepository iProjectTypeRepository)
        {
            _iProjectTypeRepository = iProjectTypeRepository;
        }
        public List<ProjectType> GetProjectType()
        {
            return _iProjectTypeRepository.All().ToList();
        }
    }
}
