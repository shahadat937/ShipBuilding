using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
  public class AgendumManager : BaseManager,IAgendumManager
    {
        private readonly IAgendumRepository _agendumRepository;
        public AgendumManager()
        {
            _agendumRepository = new AgendumRepository();
        }

      public Agendum GetAndaId(long? ajendaId)
      {
          return _agendumRepository.FindOne(x => x.Id == ajendaId);
      }

      public int Create(Agendum agendum)
      {
          return _agendumRepository.Save(agendum);
      }

      public List<Agendum> GetSearchData(string searchKey)
      {
          return _agendumRepository.Filter(x => x.FileNo.Contains(searchKey) | x.Heading.Contains(searchKey)).ToList();
      }

      public List<Agendum> GetAll()
      {
          return _agendumRepository.All().ToList();
      }
    }
}
