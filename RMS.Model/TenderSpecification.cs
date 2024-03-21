using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class TenderSpecification
    {
        public int TenderIdentity { get; set; }
        public Nullable<long> FileNo { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }
        public string Decition { get; set; }
        public bool IsApproved { get; set; }
        public string Remarks { get; set; }
        public string FileUrl { get; set; }
        public string ApproveUrl { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public string ApproveRemarks { get; set; }
        public string TenderTypeFile { get; set; }
        public Nullable<long> TenderType { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string LastUpdateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }

        public virtual CommonCode CommonCode { get; set; }
    }
}
