using System.ComponentModel.DataAnnotations;
using ICSharpCode.SharpZipLib.Checksums;
using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class TenderSpecOpinionViewModel
    {
        public TenderSpecOpinionViewModel()
        {
            TenderSpecOpinion = new TenderSpecOpinion();
            TenderSpecOpinions = new List<TenderSpecOpinion>();
            Department = new Department();
            Departments = new List<Department>();
            ProjectSelectionLists =new List<SelectionList>();
            DepartmentList =new List<SelectListItem>();
            TenderSpecDeptOpinions =new List<TenderSpecDeptOpinion>();
        }
        public TenderSpecOpinion TenderSpecOpinion { set; get; }
        public List<TenderSpecOpinion> TenderSpecOpinions { set; get; }
        public List<TenderSpecDeptOpinion> TenderSpecDeptOpinions { get; set; } 
        public List<Department> Departments { set; get; }
        public Department Department { set; get; }
        public List<SelectListItem> DepartmentSelectedList { set; get; }
        public List<SelectionList> ProjectSelectionLists { get; set; }
        public string SearchKey { set; get; }
        public string Message { set; get; }
        public int Result { set; get; }
        public string FileSerial { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public IEnumerable<SelectListItem> ProjectSelectListItems
        {
            get
            {
                return new SelectList(ProjectSelectionLists, "Value", "Text");
            }
        }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public string FormName { get; set; }
        public string FormId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
    }
}