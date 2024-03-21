using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.BLL.Manager
{
    public class FlowListManager : IFlowListManager
    {
        private readonly IFlowListRepository _iFlowListRepository;
        public FlowListManager(IFlowListRepository iFlowListRepository)
        {
            _iFlowListRepository = iFlowListRepository;

        }
        public int CreateFlowList(FlowList flowList)
        {

            return _iFlowListRepository.Save(flowList);
        }

        public List<FlowList> GetFlowList()
        {
            return _iFlowListRepository.All().ToList();
        }

        public FlowList GetFlowListByName(string flowName)
        {
            return _iFlowListRepository.FindOne(x => x.FlowName == flowName);
        }
        public FlowList GetFlowListByFlowId(int flowId)
        {
            return _iFlowListRepository.FindOne(x => x.FlowId == flowId);
        }

    }
}
