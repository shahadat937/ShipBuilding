using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class TenderOffer
    {
        public long Id { get; set; }
        public string ProjectName { get; set; }
        public string DespatchNumber { get; set; }
        public string Subject { get; set; }
        public string MainSubject { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string Including { get; set; }
        public string SigningAuthority { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
    }
}
