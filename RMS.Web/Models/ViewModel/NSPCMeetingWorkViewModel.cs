using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace RMS.Web.Models.ViewModel
{
    public class NSPCMeetingWorkViewModel
    {
        public NSPCMeetingWorkViewModel()
        {
            NSPCMeetingWork = new NSPCMeetingWork();
            NSPCMeetingWorks = new List<NSPCMeetingWork>();
        }
        public NSPCMeetingWork NSPCMeetingWork { set; get; }
        public List<NSPCMeetingWork> NSPCMeetingWorks { set; get; }
        public List<SelectListItem> CommonCodeSelectedListItems { get; set; }
        public List<SelectListItem> MemberSelectedItems { get; set; }
        public string CategoryName { get; set; }
        public string Message { set; get; }
        public string SearchKey { set; get; }
        public string OfficerName { set; get; }
    }
}