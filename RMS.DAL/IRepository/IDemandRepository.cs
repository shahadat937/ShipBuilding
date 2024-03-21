using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.DAL.IRepository
{
    public interface IDemandRepository : IRepository<Demand>
    {
        List<Demand> GetCompleteData();
        List<ProjectStatus> ProjectStatus();
    }
}
