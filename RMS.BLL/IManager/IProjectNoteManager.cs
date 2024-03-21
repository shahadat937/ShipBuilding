using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
  public interface IProjectNoteManager
    {
      List<ProjectNote> GetAll();
      ProjectNote GetById(int? id);

      int Create(ProjectNote projectNote);
      List<ProjectNote> GetAllbyId(int? pId);
      int DeleteProjectNote(int? id);
      List<ProjectNote> GetSearchValue(string searchKey);
        int? GetCompleteStatus(long ddDemandId);
    }
}
