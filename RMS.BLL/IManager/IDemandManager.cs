using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface IDemandManager
    {
        Demand GetDemandById(long? demandId);
        List<Demand> GetSearchData(string searchKey);
        int Create(Demand demand);
        List<Demand> GetAll();

        string GetProjectNameById(long p);

        List<Demand> GetCompleteProject();
        List<YearCalender> GetYearCalender();
        int CancelStatusUpdate(string demandId);
        void UpdateProjectCompleteStatus(long demandId);

        List<ProjectStatus> GetProjectStatusByYear();

        List<Demand> GetAllForIndex();
        int UndoStatusUpdate(string demandId);
        FinancialInstallment GetFinanceById(long? id);
     
    }
}
