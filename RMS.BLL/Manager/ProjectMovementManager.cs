using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.DAL;

namespace RMS.BLL.Manager
{
    public class ProjectMovementManager : IProjectMovementManager
    {
        private readonly IProjectMovementRepository _iProjectMovementRepository;
        DSBDBEntities db = new DSBDBEntities();
        public ProjectMovementManager(IProjectMovementRepository iProjectMovementRepository)
        {
            _iProjectMovementRepository = iProjectMovementRepository;

        }

        public List<ProjectFileMovement> GetProjectMovement()
        {
            return db.ProjectFileMovements.OrderBy(x=>x.ProjectId).ToList();
        }
        public List<ProjectFileMovement> GetProjectMovementByProjectId(long? ProjectId)
        {
            //var pmm = _iProjectMovementRepository.All().ToList();
            //    var mm=pmm.Where(x => x.ProjectIdentity == ProjectId).ToList();
            List<ProjectFileMovement> lst= new List<ProjectFileMovement>();
            //lst = (from c in db.ProjectFileMovements where c.ProjectIdentity == ProjectId select c).ToList();
            lst = db.ProjectFileMovements.Where(x => x.ProjectIdentity == ProjectId).ToList();
            return lst;
        }



        public ProjectMovement GetProjectMovementById(long? Id)
        {
            return _iProjectMovementRepository.FindOne(x => x.Id == Id);
        }
    }
}
