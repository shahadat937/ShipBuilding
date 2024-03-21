using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class TenderSpecDeptOpinion
    {
        public long TenderSpecOpinionGivenId { get; set; }
        public long FileNo { get; set; }
        public string TenderName { get; set; }
        public string TenderUrl { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public long OpinionGivenDeptId { get; set; }
        public string OpinionUrl { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsComplete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsOpinion { get; set; }
        public string OpinionGivenDeptName { get; set; }

        public virtual Department Department { get; set; }
        public virtual Demand Demand { get; set; }

    }
}
