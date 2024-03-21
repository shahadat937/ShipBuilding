using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class BankInfoManager:BaseManager,IBankInfoManager
    {
        private readonly IBankInfoRepository _bankInfoRepository;
        public BankInfoManager()
        {
            _bankInfoRepository = new BankInfoRepository();
        }

        public BankInfo FindOne(long branchInfoIdentity)
        {
            return _bankInfoRepository.FindOne(x => x.BranchInfoIdentity == branchInfoIdentity);
        }
    }
}
