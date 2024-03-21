using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class FileViewModel : BaseViewModel
    {
        public FileViewModel()
        {
            File = new File();
            Files = new List<File>();
            Projects = new List<Project>();
            StaffReqierment = new List<StaffRequirement>();
            Department = new List<Department>();
        }

        public List<Project> Projects { get; set; }
        public List<StaffRequirement> StaffReqierment { get; set; }
        public List<Department> Department { get; set; }
        public File File { get; set; }
        public List<File> Files { get; set; }

        public IEnumerable<SelectListItem> ProjecSelectListItems
        {
            get
            {
                return new SelectList(Projects, "ProjectId", "ProjectName");
            }
        }
        public IEnumerable<SelectListItem> StaffReqSelectListItems
        {
            get
            {
                return new SelectList(StaffReqierment, "StuffRequirementId", "Title");
            }
        }
        public IEnumerable<SelectListItem> DeptSelectListItems
        {
            get
            {
                return new SelectList(Department, "DepartmentId", "Name");
            }
        }


    }
}