//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RMS.BLL.IManager;
//using RMS.DAL.IRepository;
//using RMS.Model;

//namespace RMS.BLL.Manager
//{
//    public class StaffRequirementManager : IStaffRequirementManager
//    {
//        private readonly IStaffRequirementRepository _staffRequirementRepository;
//        public StaffRequirementManager(IStaffRequirementRepository departmentRepositoty)
//        {
//            _staffRequirementRepository = departmentRepositoty;
//        }

//        public int Create(StaffRequirement staffRequirement)
//        {
//            return _staffRequirementRepository.Save(staffRequirement);
//        }
//        public int Accept(StaffRequirement staffRequirement)
//        {
//            return _staffRequirementRepository.Edit(staffRequirement);
//        }
//        //public int Hold(StaffRequirement staffRequirement)
//        //{
//        //    return _staffRequirementRepository.Edit(staffRequirement);
//        //}
//        public List<StaffRequirement> GetApprovedStaffRequirement()
//        {
//            return _staffRequirementRepository.Filter(x => x.Status == 2).ToList();
//        }
//        public List<StaffRequirement> GetStaffRequirement()
//        {
//            return _staffRequirementRepository.All().Include(x=>x.ControlType).Include(x=>x.Status1).ToList();
//        }
//        public StaffRequirement GetStaffRequirementByTitle( string Title)
//        {
//            return _staffRequirementRepository.FindOne(x=>x.Title==Title);
//        }
//        public StaffRequirement GetStaffRequirementById(long Id)
//        {
//            return _staffRequirementRepository.FindOne(x => x.StuffRequirementId == Id);
//        }

//        public StaffRequirement GetStaffRequirementByFile(long? fileId)
//        {
//            return _staffRequirementRepository.FindOne(x => x.FileNo == fileId);
//        }
//    }
//}

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
    public class StaffRequirementManager : IStaffRequirementManager
    {
        private readonly IStaffRequirementRepository _staffRequirementRepository;
        public StaffRequirementManager(IStaffRequirementRepository departmentRepositoty)
        {
            _staffRequirementRepository = departmentRepositoty;
        }

        public int Create(StaffRequirement staffRequirement)
        {
            return _staffRequirementRepository.Save(staffRequirement);
        }
        public int Accept(StaffRequirement staffRequirement)
        {
            return _staffRequirementRepository.Edit(staffRequirement);
        }
        //public int Hold(StaffRequirement staffRequirement)
        //{
        //    return _staffRequirementRepository.Edit(staffRequirement);
        //}
        public List<StaffRequirement> GetApprovedStaffRequirement()
        {
            return _staffRequirementRepository.Filter(x => x.Status == 2).ToList();
        }
        public List<StaffRequirement> GetStaffRequirement(string projectId)
        {
            long projId = Convert.ToInt64(projectId);
            return _staffRequirementRepository.All().Where(x => x.FileNo == projId).Include(x => x.Status1).ToList();
        }
        public List<StaffRequirement> GetStaffRequirementByProject(string projectId)
        {
            long projId = Convert.ToInt64(projectId);
            return _staffRequirementRepository.All().Where(x => x.FileNo == projId).ToList();
        }
        public StaffRequirement GetStaffRequirementByTitle(string Title)
        {
            return _staffRequirementRepository.FindOne(x => x.Title == Title);
        }
        public StaffRequirement GetStaffRequirementById(long Id)
        {
            return _staffRequirementRepository.FindOne(x => x.StuffRequirementId == Id);
        }

        public StaffRequirement GetStaffRequirementByFile(long? fileId)
        {
            return _staffRequirementRepository.FindOne(x => x.FileNo == fileId);
        }
        public List<StaffRequirement> GetStaffRequirementsBySearchKey(string searchKey)
        {
            return _staffRequirementRepository.Filter(x => x.Title.ToLower().Contains(searchKey.ToLower())).Include(x =>x.Status1).ToList();
        }
    }
}
