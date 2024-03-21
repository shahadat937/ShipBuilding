using System.Collections.Generic;
using System.Linq;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;

namespace RMS.BLL.Manager
{
    public class ReportingManager : BaseManager, IReportingManager
    {
        private IReportingRepository _reportingRepository;
        public ReportingManager()
        {
            _reportingRepository = new ReportingRepository();
        }
        public List<Reporting> GetReportingTeeView()
        {
            var firstLevel = new List<Reporting>();
            var secondLevel = new List<Reporting>();
            var thirdLevel = new List<Reporting>();

            var reportingTrees = _reportingRepository.Filter(x => x.IsEventLog);

            foreach (Reporting reportingTree in reportingTrees)
            {
                if (reportingTree.ReportLevel == "1")
                {
                    firstLevel.Add(reportingTree);
                }
                if (reportingTree.ReportLevel == "2")
                {
                    secondLevel.Add(reportingTree);

                }
                if (reportingTree.ReportLevel == "3")
                {
                    thirdLevel.Add(reportingTree);
                }
            }
            foreach (Reporting reporting in firstLevel)
            {
                reporting.ReportingTrees = GetFirstChaild(reporting.SlNo, secondLevel, thirdLevel).ToList();
            }

            return firstLevel;
        }

        public Reporting GetReportParameterBySlNo(string slNo)
        {
            return _reportingRepository.FindOne(x => x.SlNo == slNo);
        }

        private IEnumerable<Reporting> GetFirstChaild(string slNo, List<Reporting> secondLevel, List<Reporting> thirdLevel)
        {
            slNo = slNo.Trim();
            secondLevel = secondLevel.Where(x => x.FirstLevel == slNo).ToList();
            foreach (var reporting in secondLevel)
            {
                reporting.ReportingTrees = GetSecondChaild(reporting.SlNo, secondLevel).ToList();
                yield return reporting;
            }

        }

        private IEnumerable<Reporting> GetSecondChaild(string slNo, List<Reporting> thirdLevel)
        {
            thirdLevel = thirdLevel.Where(x => x.SecondLevel == slNo).ToList();
            return thirdLevel;
        }
    }
}
