using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class ContractOpinion
    {
        public long OpinionId { get; set; }
        public long FileNo { get; set; }
        public string TenderName { get; set; }
        public string TenderUrl { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Message { get; set; }

        public virtual Demand Demand { get; set; }
    }
}
