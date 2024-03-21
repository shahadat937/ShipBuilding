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
    public class ProjectNoteManager :  IProjectNoteManager
    {
        private readonly IProjectNoteRepository  _projectNoteRepository;

        public ProjectNoteManager(IProjectNoteRepository projectNoteRepository)
        {
            _projectNoteRepository = projectNoteRepository;
        }

        public List<ProjectNote> GetAll()
        {
            return _projectNoteRepository.All().Include(x => x.Demand).ToList();
        }

        public ProjectNote GetById(int? id)
        {
            return _projectNoteRepository.FindOne(x => x.Id == id);
        }

        public int Create(ProjectNote projectNote)
        {
            return _projectNoteRepository.Save(projectNote);
        }

        public List<ProjectNote> GetAllbyId(int? pId)
        {
            return _projectNoteRepository.Filter(x => x.DemandId == pId).Include(x => x.Demand).ToList();
        }

        public int DeleteProjectNote(int? id)
        {
            return _projectNoteRepository.Delete(x => x.Id == id);
        }

        public List<ProjectNote> GetSearchValue(string searchKey)
        {
            return _projectNoteRepository.Filter(x => x.Demand.FileNo.Contains(searchKey) || String.IsNullOrEmpty(searchKey)).Include(x => x.Demand).ToList();
        }

        public int? GetCompleteStatus(long ddDemandId)
        {
            return  _projectNoteRepository.All().Where(x => x.DemandId == ddDemandId).Sum(x => x.Complete);
        }
    }
}
