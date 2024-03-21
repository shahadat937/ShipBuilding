using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class TenderOfferManager : ITenderOfferManager
    {
             private readonly ITenderOfferRepository _tenderOfferRepository;
             public TenderOfferManager(ITenderOfferRepository tenderOfferRepository)
        {
            _tenderOfferRepository = tenderOfferRepository;
        }

        public List<TenderOffer> GetAll()
        {
            return _tenderOfferRepository.All().ToList();
        }

        public TenderOffer GetTenderOffer(long? tenderId)
        {
            return _tenderOfferRepository.FindOne(x => x.Id == tenderId);
        }

        public int Create(TenderOffer tenderOffer)
        {
            return _tenderOfferRepository.Save(tenderOffer);
        }
    }
}
