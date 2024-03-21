using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IAgendumManager
    {
       Agendum GetAndaId(long? ajendaId);
       int Create(Agendum agendum);
       List<Agendum> GetSearchData(string searchKey);
       List<Agendum> GetAll();
    }
}
