using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
  public  interface IFlowListManager
    {
      int CreateFlowList(FlowList flowList);
      List<FlowList> GetFlowList();
      FlowList GetFlowListByName(string flowName);
      FlowList GetFlowListByFlowId(int flowId);
    }
}
