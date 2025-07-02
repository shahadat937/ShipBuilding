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
    public class DemandManager : IDemandManager
    {
        private readonly IDemandRepository _demandRepository;
        private readonly IYearCalenderRepository _yearCalenderRepository;
        private readonly IFinancialInstallmentRepository _financialInstallmentRepository;
        public DemandManager(IDemandRepository demandRepository, IYearCalenderRepository yearCalenderRepository, IFinancialInstallmentRepository financialInstallmentRepository)
        {
            _demandRepository = demandRepository;
            _yearCalenderRepository = yearCalenderRepository;
            _financialInstallmentRepository = financialInstallmentRepository;
        }

        public Demand GetDemandById(long? demandId)
        {
            return _demandRepository.FindOne(x => x.DemandId == demandId);
        }
        public List<Demand> GetSearchData(string searchKey)
        {
            return
                _demandRepository.Filter(
                    x =>
                        x.FileNo.ToLower().Contains(searchKey.ToLower()) ).ToList();
        }

        public int Create(Demand demand)
        {
            return _demandRepository.Save(demand);
        }

        public List<Demand> GetAll()
        {
            return _demandRepository.All().Include(x=>x.CommonCode).Include(x =>x.CommonCode1).Where(x =>x.IsAccept != false).Include(x =>x.ContractFiles).ToList();
        }


        public string GetProjectNameById(long p)
        {
            return _demandRepository.FindOne(x => x.DemandId == p).FileNo;
        }

        public List<Demand> GetCompleteProject()
        {
            var ss = _demandRepository.GetCompleteData();

            return _demandRepository.Filter(x => x.ProjectType == 6 && x.IsComplete == true).Include(x => x.CommonCode).Include(x => x.CommonCode1).Include(x => x.ContractFiles).ToList();
        }

        public List<YearCalender> GetYearCalender()
        {
           return _yearCalenderRepository.All().ToList();
        }

        public int CancelStatusUpdate(string demandId)
        {
            long ss = Convert.ToInt64(demandId);
            var old = _demandRepository.FindOne(x => x.DemandId == ss);
            if (old.IsComplete != true)
            {
                old.IsAccept = false;
              return  _demandRepository.Edit(old);
            }
            return 0;
        }

        public void UpdateProjectCompleteStatus(long demandId)
        {
            var old = _demandRepository.FindOne(x => x.DemandId == demandId);
            old.IsComplete = true;
            _demandRepository.Edit(old);
        }

        public List<ProjectStatus> GetProjectStatusByYear()
        {
            return _demandRepository.ProjectStatus();
        }

        public List<Demand> GetAllForIndex()
        {
            return _demandRepository.All().Include(x =>x.CommonCode).Include(x =>x.CommonCode1).Include(x =>x.ControlType).ToList();

        }

        public int UndoStatusUpdate(string demandId)
        {
            long ss = Convert.ToInt64(demandId);
            var old = _demandRepository.FindOne(x => x.DemandId == ss);
            if (old.IsComplete != true)
            {
                old.IsAccept = true;
              return  _demandRepository.Edit(old);
            }
            return 0;
        }

        public FinancialInstallment GetFinanceById(long? id)
        {
            return _financialInstallmentRepository.All().Include(x =>x.BGPG).FirstOrDefault(x => x.ProjectId == id);
        }

   
    }
}
