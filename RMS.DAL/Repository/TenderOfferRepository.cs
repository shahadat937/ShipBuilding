using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class TenderOfferRepository : Repository<TenderOffer>, ITenderOfferRepository
   {
             private readonly DSBDBEntities _context;
             public TenderOfferRepository()
                 : base()
        {
            _context = base.Context;
        }
    }
}
