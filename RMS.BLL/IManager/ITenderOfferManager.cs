using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface ITenderOfferManager
    {
       List<TenderOffer> GetAll();

       TenderOffer GetTenderOffer(long? tenderId);

       int Create(TenderOffer tenderOffer);
    }
}
