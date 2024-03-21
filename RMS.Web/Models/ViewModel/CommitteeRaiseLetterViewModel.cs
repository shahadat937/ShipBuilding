using System.ComponentModel.DataAnnotations;
using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class CommitteeRaiseLetterViewModel : BaseViewModel
    {
        public CommitteeRaiseLetterViewModel()
        {
            CommitteeRaiseLetter = new CommitteeRaiseLetter();
            CommitteeRaiseLetters = new List<CommitteeRaiseLetter>();
            ProjectLists = new List<SelectionList>();
            FinanceMinistryLetter =new FinanceMinistryLetter();
            AfdLetter =new AFDLetter();
            FininMinistryLetters = new List<FinanceMinistryLetter>();
            AFDLetters =new List<AFDLetter>();
            AFDLetterApprove = new AFDLetterApprove();
            AFDLetterApproves =new List<AFDLetterApprove>();
        }
        public AFDLetterApprove AFDLetterApprove { get; set; }
        public List<AFDLetterApprove> AFDLetterApproves { get; set; } 
        public AFDLetter AfdLetter { get; set; }
        public List<AFDLetter> AFDLetters { get; set; }
        public FinanceMinistryLetter FinanceMinistryLetter { get; set; }
        public List<FinanceMinistryLetter> FininMinistryLetters { get; set; } 
        public CommitteeRaiseLetter CommitteeRaiseLetter { set; get; }
        public List<CommitteeRaiseLetter> CommitteeRaiseLetters { set; get; }
        public List<SelectListItem> CommonCodeTypeValues { set; get; }
        public List<SelectionList> ProjectLists { get; set; } 
        public string Message { get; set; }
        public string SearchKey { get; set; }
        public string FlowSerial { get; set; }
        public string FormName { get; set; }
        public string FormId { get; set; }
        public long FileNo { get; set; }
        public IEnumerable<SelectListItem> ProjectSelectListItems
        {
            get
            {
                return new SelectList(ProjectLists, "Value", "Text");
            }
        }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase FileDocument { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ApproveDocument { get; set; }
    }
}