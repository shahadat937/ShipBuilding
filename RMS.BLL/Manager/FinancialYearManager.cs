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
   public class FinancialYearManager : IFinancialYearManager
    {
            private readonly IFinancialYearRepository  _financialYearRepository;

            public FinancialYearManager(IFinancialYearRepository financialYearRepository)
            {
                      _financialYearRepository = financialYearRepository;
            }

            public FinancialYear GetFinantialInstallment(int? id)
       {
           return _financialYearRepository.FindOne(x => x.Id == id);
       }

       public int Save(FinancialYear oldData)
       {
           return _financialYearRepository.Save(oldData);
       }

       public List<FinancialYear> GetAll()
       {
           return _financialYearRepository.All().ToList();
       }

       public int DeleteFinantialYear(int finantialYearId)
       {
           return _financialYearRepository.Delete(x => x.Id == finantialYearId);
       }
    }
}
