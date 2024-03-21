using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class DemandViewModel : BaseViewModel
    {
        public Demand Demand { get; set; }
        public List<Demand> Demands { get; set; }
        public ProjectNote ProjectNote { get; set; }
        public List<ProjectNote> ProjectNotes { get; set; }
        public List<SelectionList> Departments { get; set; }
        public List<SelectionList> ProjectType { get; set; }
        public List<SelectionList> ApprovalType { get; set; }
        public List<SelectionList> ProjectList { get; set; }
        public List<SelectionList> ProjectCategory { get; set; }
        public List<SelectionStringList> YearCalender { get; set; }
        public List<ControlType> ControlTypes { get; set; } 
        public DemandViewModel()
        {
            ProjectNote =new ProjectNote();
            ProjectNotes = new List<ProjectNote>();
            Demand =new Demand();
            Demands =new List<Demand>();
            Departments = new List<SelectionList>();
            ProjectType =new List<SelectionList>();
            ApprovalType =new List<SelectionList>();
            ProjectCategory =new List<SelectionList>();
            YearCalender = new List<SelectionStringList>();
            ControlTypes =new List<ControlType>();
            ProjectList =new List<SelectionList>();
        }
        public string SearchKey { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase DemandingUpload { get; set; }
        public IEnumerable<SelectListItem> ApprovalTypeSelectListItems
        {
            get
            {
                return new SelectList(ApprovalType, "Value", "Text");
            }
        }

        public IEnumerable<SelectListItem> ProjectListSelectListItems
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
                return new SelectList(Departments, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> ProjectTypeSelectListItem
        {
            get
            {
                return new SelectList(ProjectType, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> ProjectCategorySelectListItems
        {
            get
            {
                return new SelectList(ProjectCategory, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> YearCalenderSelectListItem
        {
            get
            {
                return new SelectList(YearCalender, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> ControlTypeSelectList
        {
            get
            {
                return new SelectList(ControlTypes, "Id", "ControlName");
            }
        }
    }
}