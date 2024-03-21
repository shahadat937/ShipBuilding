using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
  public class WithdrawalLetterInfo
    {
        public long Id { get; set; }
        public string FileNo { get; set; }
        public Nullable<long> Category { get; set; }
        public string LMNo { get; set; }
        public string Subject { get; set; }
        public string Formula { get; set; }
        public string ApprovalAuthority { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        public string Attached { get; set; }
        public string intimation { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
    }
}
