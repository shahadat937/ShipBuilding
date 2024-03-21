using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class FileMovementViewModel : BaseViewModel
    {
        public FileMovementViewModel()
        {
            FileMovement=new FileMovement();
            FileMovements = new List<FileMovement>();
            ProjectMovements = new List<ProjectMovement>();
            ProjectFileMovements = new List<ProjectFileMovement>();
            FutureProject = new List<ProjectFileMovement>();
            OngoingProject = new List<ProjectFileMovement>();
            ProjectFileMovements1 = new ProjectFileMovement();
            Departments = new List<Department>();
            Files = new List<File>();
            Projects = new List<Project>();
            Project = new Project();
            FlowSetups = new List<FlowSetup>();
            FlowSetup = new FlowSetup();
            FlowLists=new List<FlowList>();
            StaffRequirements = new List<StaffRequirement>();
            CommonCodes = new List<CommonCode>();
            ProjectList = new List<SelectionList>();
            Demands =new List<Demand>();
            Demand =new Demand();
        }
        public List<Demand> Demands { get; set; }
        public Demand Demand { get; set; }
        public FileMovement FileMovement { get; set; }
        public List<SelectionList> ProjectList { get; set; } 
        public List<FileMovement> FileMovements { get; set; }
        public List<ProjectMovement> ProjectMovements { get; set; }
        public List<ProjectFileMovement> ProjectFileMovements { get; set; }
        public List<ProjectFileMovement> FutureProject { get; set; }
        public List<ProjectFileMovement> OngoingProject { get; set; }
        public ProjectFileMovement ProjectFileMovements1 { get; set; }
        public List<Department> Departments { get; set; }
        public List<File> Files { get; set; }
        public List<Project> Projects { get; set; }
        public Project Project { get; set; }
        public List<FlowSetup> FlowSetups { get; set; }
        public FlowSetup FlowSetup { get; set; }
        public List<FlowList> FlowLists { get; set; }
        public List<StaffRequirement> StaffRequirements { get; set; }
        public List<CommonCode> CommonCodes { get; set; }

        public IEnumerable<SelectListItem> DeptSelectListItems
        {
            get
            {
                return new SelectList(Departments, "DepartmentId", "Name");
            }
        }
        public IEnumerable<SelectListItem> StaffSelectListItems
        {
            get
            {
                return new SelectList(StaffRequirements, "StuffRequirementId", "Title");
            }
        }
        public IEnumerable<SelectListItem> FilesSelectListItems
        {
            get
            {
                return new SelectList(Files, "FileId", "FileName");
            }
        }
        public IEnumerable<SelectListItem> FlowSelectListItems
        {
            get
            {
                return new SelectList(FlowLists, "FlowId", "FlowName");
            }
        }
        public IEnumerable<SelectListItem> ProjectSelectListItems
        {
            get
            {
                return new SelectList(Projects, "ProjectIdentity", "ProjectName");
            }
        }
        public IEnumerable<SelectListItem> ComSelectListItems
        {
            get
            {
                return new SelectList(CommonCodes, "CommonCodeID", "TypeValue");
            }
        }
        public IEnumerable<SelectListItem> ProjectSelectListItem
        {
            get
            {
                return new SelectList(ProjectList, "Value", "Text");
            }
        }
      
    }

}