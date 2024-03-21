using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class TenderSecificationRepository : Repository<TenderSpecification>, ITenderSecificationRepository
   {
                private readonly DSBDBEntities _context;
                public TenderSecificationRepository()
                    : base()
        {
            _context = base.Context;
        }
    }
}
