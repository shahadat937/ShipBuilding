using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public partial class AFDLetter
    {
       public AFDLetter()
        {
            this.AFDLetterApproves = new HashSet<AFDLetterApprove>();
        }

        public long LetterId { get; set; }
        public long FileNo { get; set; }
        public long CommonCodeTypeValueId { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string AttachedDocument { get; set; }
        public string DescriptionBody { get; set; }
        public string FromAuthority { get; set; }
        public string ToAuthority { get; set; }
        public string InformedAuthority { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string CommonCodeTypeValue { get; set; }

        public virtual Demand Demand { get; set; }
        public virtual ICollection<AFDLetterApprove> AFDLetterApproves { get; set; }

    }
}
