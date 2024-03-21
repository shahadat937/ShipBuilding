using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class ContractSignManager : BaseManager,IContractSignManager
    {
        private readonly IContractSignRepository _contractSignRepository;

        public ContractSignManager()
        {
            _contractSignRepository = new ContractSignRepository();
           
        }

        public List<ContractSign> GetAllbyId(long? proId)
        {
            return _contractSignRepository.All().Where(x =>x.Demandid == proId).Include(x=>x.Demand).ToList();

        }

        public ContractSign GetById(long? id)
        {
            return _contractSignRepository.FindOne(x => x.Demandid == id);
        }

        public int Create(ContractSign ContractSign)
        {
            return _contractSignRepository.Save(ContractSign);
        }

 
    }
}
