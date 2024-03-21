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
    public class StaffRequirementViewModel:BaseViewModel
    {
        public StaffRequirementViewModel()
        {
            StaffRequirement = new StaffRequirement();
            StaffRequirements = new List<StaffRequirement>();
          
            Statuses = new List<Status>();
            FlowLists =new List<SelectionList>();
            CommitteList =new List<SelectionList>();
            ProjectList =new List<SelectionList>();
        }
        public List<SelectionList> ProjectList { get; set; } 
        public List<SelectionList> FlowLists { get; set; }
        public List<SelectionList> CommitteList { get; set; } 
        public HttpPostedFileBase Document { get; set; }
        public StaffRequirement StaffRequirement { get; set; }
        public List<StaffRequirement> StaffRequirements { get; set; }

        public List<Status> Statuses { get; set; }
        public string ValidationCheckBySubject { get; set; }
        public string FlowSerial { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ApproveDocument { get; set; }

        public IEnumerable<SelectListItem> StatusSelectList
        {
            get
            {
                return new SelectList(Statuses, "Id", "FileStatus");
            }
        }
        public IEnumerable<SelectListItem> FlowSelectListItems
        {
            get
            {
                return new SelectList(FlowLists, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> CommitteSelectListItems
        {
            get
            {
                return new SelectList(CommitteList, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> ProjectSelectList
        {
            get
            {
                return new SelectList(ProjectList, "Value", "Text");
            }
        }

        public string SearchKey { get; set; }
    }
}