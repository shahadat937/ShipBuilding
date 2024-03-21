using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class ProjectViewModel:BaseViewModel
    {
        public Project Project { get; set; }
        public ContractSign ContractSign { get; set; }
        public List<ProjectNote> ProjectNotes { get; set; }
        public List<FlowSetup> ProjectFinalStatus { get; set; } 
        public FileMovement FileMovement { get; set; }
        public List<Project> Projects { get; set; }
        public List<ControlType> ProjectTypes { get; set; }
        public List<StaffRequirement> StaffRequirements { get; set; }
        public List<Department> Departments { get; set; }
        public List<Status> Status { get; set; }
        public List<ProjectStatu> ProjectStatus { get; set; }
        public List<FlowList> ProjectFlow { get; set; }
        public List<Department> SelectedDepartments { get; set; }
        public List<ProjectStatus> AllProjectStatus { get; set; }
        public List<ProjectStatu> PendingInfoes { get; set; }
        public spProjectTaskStatus SpProjectTaskStatuse { get; set; }       
        public List<Group<string, CommitteInfo>> AllCommitteListed { get; set; }
        public List<Group<string, spProjectOpinion_Result>> AllOpinion { get; set; }
        public List<FinancialInstallment> FinancialInstallments { get; set; }
        public ProjectViewModel()
        {
            ProjectNotes = new List<ProjectNote>();
            ContractSign = new ContractSign(); 
            Project = new Project();
            FileMovement = new FileMovement();
            Projects = new List<Project>();
            StaffRequirements = new List<StaffRequirement>();
            Departments = new List<Department>();
            Status = new List<Status>();
            ProjectStatus = new List<ProjectStatu>();
            ProjectFlow = new List<FlowList>();
            SelectedDepartments = new List<Department>();
            ProjectTypes=new List<ControlType>();
            ProjectFinalStatus = new List<FlowSetup>();
            AllProjectStatus = new List<ProjectStatus>();
            PendingInfoes =new List<ProjectStatu>();
            SpProjectTaskStatuse =new spProjectTaskStatus();
            AllCommitteListed = new List<Group<string, CommitteInfo>>();
            AllOpinion = new List<Group<string, spProjectOpinion_Result>>();
            FinancialInstallments =new List<FinancialInstallment>();
        }
        public string ProjectCount { get; set; }
        public string OngoingProject { get; set; }
        public string CutrrentProject { get; set; }
        public string FutureProject { get; set; }
        public string CompleteProject { get; set; }
        public string ProjectId { get; set; }
 

        public IEnumerable<SelectListItem> StaffRequirementSelectList
        {
            get
            {
                return new SelectList(StaffRequirements, "StuffRequirementId", "Title");
            }
        }

        public IEnumerable<SelectListItem> DepartmentSelectList
        {
            get
            {
                return new SelectList(Departments, "DepartmentId", "Name");
            }
        }
        public IEnumerable<SelectListItem> SelectedDepartmentSelectList
        {
            get
            {
                return new SelectList(SelectedDepartments, "DepartmentId", "Name");
            }
        }
        public IEnumerable<SelectListItem> ProjectTypeSelectList
        {
            get
            {
                return new SelectList(ProjectTypes, "Id", "ControlName");
            }
        }
        public IEnumerable<SelectListItem> StatusSelectList
        {
            get
            {
                return new SelectList(ProjectStatus, "Id", "ProjectStatus");
            }
        }
        public IEnumerable<SelectListItem> FlowSelectList
        {
            get
            {
                return new SelectList(ProjectFlow, "FlowId", "FlowName");
            }
        }
       
    }
}