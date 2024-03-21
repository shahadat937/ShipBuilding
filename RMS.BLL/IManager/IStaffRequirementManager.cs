//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using RMS.Model;

//namespace RMS.BLL.IManager
//{
//    public interface IStaffRequirementManager
//    {
//        int Create(StaffRequirement staffRequirement);
//        int Accept(StaffRequirement staffRequirement);
//        List<StaffRequirement> GetApprovedStaffRequirement();
//        List<StaffRequirement> GetStaffRequirement();
//        StaffRequirement GetStaffRequirementByTitle(string Title);
//        StaffRequirement GetStaffRequirementById(long Id);
//        StaffRequirement GetStaffRequirementByFile(long? fileId);
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMS.Model;

namespace RMS.BLL.IManager
{
    public interface IStaffRequirementManager
    {
        int Create(StaffRequirement staffRequirement);
        int Accept(StaffRequirement staffRequirement);
        List<StaffRequirement> GetApprovedStaffRequirement();
        List<StaffRequirement> GetStaffRequirement(string projectId);
        StaffRequirement GetStaffRequirementByTitle(string Title);
        StaffRequirement GetStaffRequirementById(long Id);
        StaffRequirement GetStaffRequirementByFile(long? fileId);
        List<StaffRequirement> GetStaffRequirementsBySearchKey(string searchKey);

        List<StaffRequirement> GetStaffRequirementByProject(string projectId);
    }
}

