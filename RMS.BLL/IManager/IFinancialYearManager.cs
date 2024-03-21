using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
   public interface IFinancialYearManager
    {
       FinancialYear GetFinantialInstallment(int? id);
       int Save(FinancialYear oldData);
       List<FinancialYear> GetAll();
       int DeleteFinantialYear(int finantialYearId);
    }
}
