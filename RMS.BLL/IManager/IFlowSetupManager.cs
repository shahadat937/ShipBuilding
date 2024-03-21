using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.TreeView;
using RMS.Model;

namespace RMS.BLL.IManager
{
  public  interface IFlowSetupManager
    {
      int CreateFlowSetup(FlowSetup flowSetup);
      List<FlowSetup> GetFlowSetup();
  
      FlowSetup GetFlowSetupByFileId(long FlowSetupId);
      List<FlowSetup> GetFlowSetupByFlowId(int FlowId);


      int UpdateFlowSetup(long identity, long depId, int flowId, string flowSequence);
      string GetFlowSerialNo(long? flowId);
   
      void DeleteWithOrderby(long? identity, string flowid);
      List<FileCrafts> GetChartOfAccount();
      FlowSetup GetFlowSetupByIdentity(long flowIdentity);
      FlowSetup GetFlowSetupByFlowSerial(long fileNo, string flowserial);
      List<FlowSetup> GetFlowSetupByFlowNo(long fileNo);
      string GetTaskStatus(string s, long? flowId);
      void UpdateFormStatusInFlow(long? fileNo, string flowSerial, string formNo,Nullable<DateTime> actEndDate);
      List<FlowSetup> GetAll();
      Task<PendingDepartmentStatus_Result> GetPendingDays(long demandId);
    }
}
