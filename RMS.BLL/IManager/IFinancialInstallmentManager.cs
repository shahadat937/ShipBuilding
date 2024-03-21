using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
  public interface IFinancialInstallmentManager
    {
      List<FinancialInstallment> GetFinancialInstallment(long? projectId);

      void DeleteFinancialInstallmentByProjectId(long demandId);
      string SaveFinancialInstallments(List<FinancialInstallment> projectInstallments, long demandId);
      List<FinancialInstallment> GetAllByProjectId(string projectId);
      void DeleteFinancialInstallment(long? financialID);

      string SaveBgpg(BGPG bgpg);
    }
}
