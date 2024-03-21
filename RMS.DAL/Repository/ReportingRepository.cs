using RMS.DAL.IRepository;
using RMS.Model;

namespace RMS.DAL.Repository
{
    public class ReportingRepository : Repository<Reporting>, IReportingRepository
    {
        public ReportingRepository() : base()
        {
        }
    }
}
