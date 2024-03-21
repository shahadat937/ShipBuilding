using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class BankInfoRepository:Repository<BankInfo>,IBankInfoRepository
    {
        public BankInfoRepository() : base()
        {
        }
    }
}
