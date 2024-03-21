using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class spProjectOpinion_Result
    {
        public string TaskName { get; set; }
        public string ProjectName { get; set; }
        public string TenderName { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string DeptName { get; set; }
        public string OpinionUrl { get; set; }
        public string Remarks { get; set; }
    }
}
