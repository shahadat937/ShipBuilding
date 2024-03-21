using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class FlowSetupRepository : Repository<FlowSetup>, IFlowSetupRepository
    {
        private readonly DSBDBEntities _context;

        public FlowSetupRepository()
            : base()
        {
            _context = base.Context;
        }

        public List<FileListByTree_Result> GetChartOfAc()
        {
            return Context.FileListByTree().Where(x=>x.IsSkip != true).ToList();

        }

        public Task<PendingDepartmentStatus_Result> GetPendingDays(long demandId)
        {
            return Context.Database.SqlQuery<PendingDepartmentStatus_Result>(" exec PendingDepartment " + demandId + "").FirstOrDefaultAsync();
        }
    }
}
