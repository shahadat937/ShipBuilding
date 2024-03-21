using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
  public class CommitteInfo
    {
        public long CommiteeId { get; set; }
        public Nullable<long> FileNo { get; set; }
        public long CommitteName { get; set; }
        public long MemberId { get; set; }
        public string WorkPlace { get; set; }
        public long CommitteDesignation { get; set; }
        public string UserId { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

        public virtual CommitteNameList CommitteNameList { get; set; }
        public virtual CommonCode CommonCode { get; set; }
        public virtual Department Department { get; set; }
        public virtual Member Member { get; set; }
    }
}
