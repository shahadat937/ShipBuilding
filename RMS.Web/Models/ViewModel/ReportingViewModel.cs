using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Model;
using RMS.Utility;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class ReportingViewModel:Reporting
    {
        public List<Reporting> ReportingTreeViews { get; set; }
        public List<BranchInfo> ExchangeCompany { get; set; }
        //public List<ControlShipInfo> ShipInfoes { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string ReportName { get; set; }
        public List<CommonCode> Ranks { get; set; }
        public List<CommonCode> NameOfCourses { get; set; }
        public ReportingViewModel()
        {
            ReportingTreeViews = new List<Reporting>();
            ExchangeCompany = new List<BranchInfo>();
            Parameters=new Dictionary<string, string>();
            Ranks = new List<CommonCode>();
            NameOfCourses = new List<CommonCode>();
        }
        public IEnumerable<SelectListItem> ExchangeCompanyListItem
        {
            get { return new SelectList(ExchangeCompany, "BranchCode", "BranchName"); }
        }
        
        public IEnumerable<SelectListItem> RanksListItems
        {
            get { return new SelectList(Ranks, "CommonCodeID", "TypeValue"); }

        }
        public IEnumerable<SelectListItem> NameOfCoursesListItems
        {
            get { return new SelectList(NameOfCourses, "CommonCodeID", "TypeValue"); }

        }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

    
    }
}