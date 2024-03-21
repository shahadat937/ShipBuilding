using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class ControlTypeRepository:Repository<ControlType>,IControlTypeRepository
    {
       private readonly DSBDBEntities _context;
        public ControlTypeRepository():base()
        {
            _context = base.Context;
        }

    }
}
