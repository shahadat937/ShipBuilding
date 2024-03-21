using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.BLL.IManager
{
    public interface IProjectInstallmentManager
    {
        List<ProjectInstallment> GetProjectInstallmentsById(long projectId);

        ProjectInstallment GetProjectInstallmentById(long projectInstallmentId);

        string SavePaymentInstallment(ProjectInstallment projectInstallment);


        void DeleteProjectInstallment(ProjectInstallment projectInstallment);

        void DeleteProjectInstallmentByProjectId(long? projectId);

        string SaveProjectInstallments(List<ProjectInstallment> list, long financialInstallmentId);

        decimal GetTotalInstallmentAmountByProjectId(long? projectId);

        decimal GetInstallmentAmountById(long installmentId);
        void DeleteProjectInstallmentByFinancialId(long financialInstallmentId);
        decimal? GetProjectAmount(long? id);

        void UpdateStatus(long? installmentId,DateTime? createDate);
    }
}
