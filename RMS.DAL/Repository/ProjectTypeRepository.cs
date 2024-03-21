using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ProjectTypeRepository : Repository<ProjectType>, IProjectTypeRepository
    {
        private readonly DSBDBEntities _context;
        public ProjectTypeRepository():base()
        {
            _context = base.Context;
        }
    }
}
