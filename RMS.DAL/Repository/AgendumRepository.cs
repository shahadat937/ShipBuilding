using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class AgendumRepository : Repository<Agendum>, IAgendumRepository
  {
        public AgendumRepository()
           : base()
        {
        }
    }
}
