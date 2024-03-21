using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Spreadsheet;
using RMS.Model;
using RMS.Utility;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class CommiteeViewModel : BaseViewModel
    {
        public FlowList FlowList { get; set; }
        public CommitteNameList CommitteNameList { get; set; }
        public CommitteInfo CommitteInfo { get; set; }
        public List<CommitteInfo> CommitteInfos { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public List<StaffRequirement> StaffReqierment { get; set; }
        public List<CommitteNameList> CommitteName { get; set; }
        public List<Department> Departments { get; set; }
        public List<CommitteInfo> AllCommitteListed { get; set; }
        public List<SelectionList> CommitteDesignation { get; set; }
        public List<SelectionList> ProjectList { get; set; }
        public CommiteeViewModel()
        {
            CommitteInfo = new CommitteInfo();
            CommitteInfos = new List<CommitteInfo>();
            StaffReqierment = new List<StaffRequirement>();
            CommitteName = new List<CommitteNameList>();
            CommitteNameList =new CommitteNameList();
            FlowList =new FlowList();
            Departments =new List<Department>();
            AllCommitteListed =new List<CommitteInfo>();
            CommitteDesignation = new List<SelectionList>();
            ProjectList =new List<SelectionList>();
        }
        public string MemberName { get; set; }
        public int Result { get; set; }
        public string FlowSerial { get; set; }
        public IEnumerable<SelectListItem> ProjectSelectListItems
        {
            get
            {
                return new SelectList(ProjectList, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> StaffReqSelectListItems
        {
            get
            {
                return new SelectList(StaffReqierment, "StuffRequirementId", "Title");
            }
        }
        public IEnumerable<SelectListItem> CommitteSelectListItems
        {
            get
            {
                return new SelectList(CommitteName, "Id", "CommitteName");
            }
        }
        public IEnumerable<SelectListItem> DeptSelectListItems
        {
            get
            {
                return new SelectList(Departments, "DepartmentId", "Name");
            }
        }
        public IEnumerable<SelectListItem> CommitteDesignationListItems
        {
            get
            {
                return new SelectList(CommitteDesignation, "Value", "Text");
            }
        }
    }
}