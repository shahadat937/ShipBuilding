//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using RMS.Model;
//using System.Web.Mvc;
//using RMS.Utility;

//namespace RMS.Web.Models.ViewModel
//{
//    public class MemberViewModel : BaseViewModel
//    {
//        public Member Member { get; set; }
//        public List<Member> Members { get; set; }
//        public List<CommitteInfo> Commitee { get; set; }
//        public HttpPostedFileBase Image { get; set; }
//        public List<SelectionList> RankLists { get; set; } 
//        public MemberViewModel()
//        {
//            Member=new Member();
//            Members = new List<Member>();
//            Commitee = new List<CommitteInfo>();
//            RankLists =new List<SelectionList>();
//        }
       
//        public IEnumerable<SelectListItem> CommiteeSelectListItems
//        {
//            get
//            {
//                return new SelectList(Commitee, "CommiteeId", "Name");
//            }
//        }
//        public IEnumerable<SelectListItem> RankSelectListItems
//        {
//            get
//            {
//                return new SelectList(RankLists, "Value", "Text");
//            }
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Model;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class MemberViewModel : BaseViewModel
    {
        public Member Member { get; set; }
        public List<Member> Members { get; set; }
        public List<CommitteInfo> Commitee { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public List<SelectionList> RankLists { get; set; }
        public MemberViewModel()
        {
            Member = new Member();
            Members = new List<Member>();
            Commitee = new List<CommitteInfo>();
            RankLists = new List<SelectionList>();
        }
        public string SearchKey { set; get; }
        public IEnumerable<SelectListItem> CommiteeSelectListItems
        {
            get
            {
                return new SelectList(Commitee, "CommiteeId", "Name");
            }
        }
        public IEnumerable<SelectListItem> RankSelectListItems
        {
            get
            {
                return new SelectList(RankLists, "Value", "Text");
            }
        }
    }
}