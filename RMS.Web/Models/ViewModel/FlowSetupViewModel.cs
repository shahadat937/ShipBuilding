using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.PowerShell.Commands;
using RMS.BLL.TreeView;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class FlowSetupViewModel : BaseViewModel
    {
        public FlowSetupViewModel()
        {
            File = new File();
            FlowSetup = new FlowSetup();
            FlowList = new FlowList();
            Files = new List<File>();
            FlowSetupList = new List<FlowSetup>();
            FlowSetups = new List<FlowSetup>();
            ProjectList = new List<SelectionList>();
            Projects = new List<Project>();
            StaffReqierment = new List<StaffRequirement>();
            Department = new List<Department>();
            FlowNameList =new List<VwFlowName>();
            ChartofAccounts = new List<FileCrafts>();
            FlowSetupDictionary = new Dictionary<string, ContractField>();
            FormInfos = new List<SelectionList>();
            FlowSetupLists = new List<SelectionList>();
            FlowSetupEditDictionary =new Dictionary<string, SelectionList>();
           
        }
        public List<SelectionList> FlowSetupLists { get; set; }
        public List<SelectionList> FormInfos { get; set; }
        public string Key { get; set; }
        public long DeptId { get; set; }
        public Dictionary<string, ContractField> FlowSetupDictionary { get; set; }
        public Dictionary<string, SelectionList> FlowSetupEditDictionary { get; set; }
        public List<FileCrafts> ChartofAccounts { get; set; }
        public List<VwFlowName> FlowNameList { get; set; } 
        public List<Project> Projects { get; set; }
        public List<StaffRequirement> StaffReqierment { get; set; }
        public List<Department> Department { get; set; }
      
        public FlowSetup FlowSetup { get; set; }
        public FlowList FlowList { get; set; }
        public List<SelectionList> ProjectList { get; set; }
        public List<FlowSetup> FlowSetups { get; set; }
        public List<FlowSetup> FlowSetupList { get; set; }
        public File File { get; set; }
        public List<File> Files { get; set; }
        public string[] DepId { get; set; }
     
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
        public IEnumerable<SelectListItem> ProjectSelectListItem
        {
            get
            {
                return new SelectList(ProjectList, "Value", "Text");
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