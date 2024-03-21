using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.DAL.IRepository
{
  public  interface IFlowSetupRepository:IRepository<FlowSetup>
  {
      List<FileListByTree_Result> GetChartOfAc();
      Task<PendingDepartmentStatus_Result> GetPendingDays(long demandId);
  }
}
