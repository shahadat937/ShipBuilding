using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.BLL.TreeView;
using RMS.DAL;
using RMS.DAL.IRepository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class FlowSetupManager : IFlowSetupManager
    {
        private readonly IFlowSetupRepository _iFlowSetupRepository;
        private readonly IDemandRepository _demandRepository;
        public FlowSetupManager(IFlowSetupRepository iFlowSetupRepository, IDemandRepository demandRepository)
        {
            _iFlowSetupRepository = iFlowSetupRepository;
            _demandRepository = demandRepository;

        }
        public int CreateFlowSetup(FlowSetup flowSetup)
        {
            return _iFlowSetupRepository.Save(flowSetup);
        }

        public List<FlowSetup> GetFlowSetup()
        {
            return _iFlowSetupRepository.All().ToList();
        }

   
        public List<FlowSetup> GetFlowSetupByFlowId(int FlowId)
        {
            return _iFlowSetupRepository.Filter(x => x.FlowId == FlowId).Include(x=>x.Department).ToList();
        }

        public int UpdateFlowSetup(long identity, long depId, int flowId, string flowSequence)
        {
            FlowSetup flowList = _iFlowSetupRepository.FindOne(x => x.FlowId == flowId && x.FlowSetupIdentity == identity);
            if (flowList != null)
            {
                flowList.DepartmentId = depId;
                flowList.FlowSerial = flowSequence;

            }
            return _iFlowSetupRepository.Save(flowList);
        }

        public string GetFlowSerialNo(long? flowId)
        {
            int no =1;
            no += _iFlowSetupRepository.Count(x => x.FlowId == flowId);
            return no.ToString();
        }

   

        public void DeleteWithOrderby(long? identity, string flowid)
        {
             _iFlowSetupRepository.Delete(x => x.FlowSetupIdentity == identity);
            int flid = Convert.ToInt32(flowid);
            var oldData = _iFlowSetupRepository.Filter(x => x.FlowId == flid).OrderBy(x => x.FlowSerial).ToList();
            var counter = 1;
            foreach (var item in oldData)
            {
                var getdata = _iFlowSetupRepository.FindOne(x =>x.FlowSetupIdentity == item.FlowSetupIdentity);
                getdata.FlowSerial = counter++.ToString();
                _iFlowSetupRepository.Edit(getdata);
            }
        }
        public FlowSetup GetFlowSetupByFlowSerial(long fileNo, string flowserial)
        {
            return _iFlowSetupRepository.FindOne(x => x.FlowSerial.Trim() == flowserial.Trim() && x.FlowId == fileNo);
        }
        public List<FileCrafts> GetChartOfAccount()
        {
            var GetList = _iFlowSetupRepository.GetChartOfAc().OrderBy(x => x.Level).ThenBy(x=>x.FlowSerial).ThenBy(x =>x.taskSerial).ToList();
            //var allPro = _demandRepository.All().Where(x => x.IsAccept != false);
          
          
           // var controlAccounts = _selfControlInfoRepository.Filter(x => x.IsActive).OrderBy(x => x.ControlLevel).ThenBy(x => x.SelfId).ToList();
            IFileTarget chTarget = new FileChartofAccountAdapter(GetList);
            var client = new FileTreeViewBuilder(chTarget);
            return client.GetChartofAccount();
        }

        public FlowSetup GetFlowSetupByIdentity(long flowIdentity)
        {
            return _iFlowSetupRepository.FindOne(x => x.FlowSetupIdentity == flowIdentity);
        }

        public List<FlowSetup> GetFlowSetupByFlowNo(long fileNo)
        {
            return _iFlowSetupRepository.Filter(x => x.FlowId == fileNo).ToList();
        }

        public string GetTaskStatus(string s, long? flowId)
        {
            var data = _iFlowSetupRepository.Filter(x => x.FlowId == flowId);
           
            foreach (var item in data)
            {
                int p = 0;
                string[] form = item.FormId.Split(',');
                string[] isStatus = item.IsComplete.Split(',');
                foreach (var ss in form)
                {
                    if (form[p] == s && isStatus[p].ToLower() == "true") return "true";
                }
            }
            return "false";
        }

        public void UpdateFormStatusInFlow(long? fileNo, string flowSerial, string formNo,Nullable<DateTime> actEndDate )
        {
            if (fileNo != null && flowSerial != null)
            {
               
                //List<FlowSetup> flowSetup = _flowSetupManager.GetFlowSetupByFlowNo(Convert.ToInt64(model.StaffRequirement.FileNo));
                List<FlowSetup> flowSetup = _iFlowSetupRepository.Filter(x => x.FlowId == fileNo).ToList(); ;
                foreach (var setup in flowSetup)
                {
                    string[] formId = setup.FormId.Split(',');
                    string[] status = setup.IsComplete.Split(',');
                    string[] skipStatus = setup.SkipStatus.Split(',');
                    List<bool> isComplete = new List<bool>();
                    List<bool> isCompleteAllStatus = new List<bool>();
                    int p = 0;
                    foreach (var item in formId)
                    {
                        if (item.Trim() == formNo.Trim() &&
                            setup.FlowSerial.Trim() == flowSerial.Trim())
                        {
                            isComplete.Add(true);
                            isCompleteAllStatus.Add(true);
                        }
                        else if (item.Trim() == formNo.Trim() && setup.FlowSerial.Trim() != flowSerial.Trim())
                        {
                            isCompleteAllStatus.Add(true);
                            isComplete.Add(Convert.ToBoolean(status[p]));
                        }
                        else
                        {
                            isCompleteAllStatus.Add(Convert.ToBoolean(skipStatus[p]));
                            isComplete.Add(Convert.ToBoolean(status[p]));
                        }
                        p++;
                    }
                    setup.IsComplete = string.Join(",", isComplete);
                    setup.SkipStatus = string.Join(",", isCompleteAllStatus);
                    setup.ActDepEndDate = actEndDate ?? setup.ActDepEndDate;
                    _iFlowSetupRepository.Save(setup);
                }

            }
        }

        public List<FlowSetup> GetAll()
        {
            return _iFlowSetupRepository.All().Include(x =>x.Department).ToList();
        }

        public Task<PendingDepartmentStatus_Result> GetPendingDays(long demandId)
        {
            return _iFlowSetupRepository.GetPendingDays(demandId);
        }


        public FlowSetup GetFlowSetupByFileId(long Id)
        {
            return _iFlowSetupRepository.FindOne(x => x.FlowSetupIdentity == Id);
        }
    }
}
