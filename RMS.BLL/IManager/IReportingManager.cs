using System.Collections.Generic;
using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface IReportingManager
    {
        List<Reporting> GetReportingTeeView();
        Reporting GetReportParameterBySlNo(string slNo);
    }
}
