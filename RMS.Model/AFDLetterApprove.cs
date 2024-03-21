using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class AFDLetterApprove
    {
        public long Id { get; set; }
        public Nullable<long> AFDLetter { get; set; }
        public bool IsApprove { get; set; }
        public string ApproveUrl { get; set; }
        public string Reference { get; set; }
        public string Decition { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }

        public virtual AFDLetter AFDLetter1 { get; set; }
    }
}
