using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
   public class DemandRepository : Repository<Demand>,IDemandRepository
   {
       public DemandRepository()
           : base()
        {
        }

       public List<Demand> GetCompleteData()
       {
           return Context.Database.SqlQuery<Demand>("select a.* from Demand a left join ContractFile b on a.DemandId = b.DemandId where a.ProjectType = 6 and b.IsApproved = '1'").ToList();
       }

       public List<ProjectStatus> ProjectStatus()
       {
           return Context.Database.SqlQuery<ProjectStatus>("select EndDate,COUNT(DemandId) TotalProject,COUNT(CompleteProject) CompleteProject from (select EndDate,DemandId,(select DemandId from Demand b where a.DemandId = b.DemandId and b.IsComplete = 'true') as CompleteProject from Demand a ) aa group by EndDate").ToList();

       }
   }
}
