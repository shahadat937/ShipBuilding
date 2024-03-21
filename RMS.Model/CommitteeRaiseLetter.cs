using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class CommitteeRaiseLetter
    {
        public long CommitteeRaiseLetterId { get; set; }
        public long FileNo { get; set; }
        public long CommonCodeTypeValueId { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string AttachedDocument { get; set; }
        public string FromAuthority { get; set; }
        public string ToAuthority { get; set; }
        public string InformedAuthority { get; set; }
        public bool IsApprove { get; set; }
        public string ApproveUrl { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CommonCodeTypeValue { get; set; }

        public virtual Demand Demand { get; set; }



    }
}
