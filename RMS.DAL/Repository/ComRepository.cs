using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ComRepository : Repository<CommitteInfo>, IComRepository
    {
         private readonly DSBDBEntities _context;
         public ComRepository()
            : base()
        {
            _context = base.Context;
        }
         
    }
}