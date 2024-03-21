using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class NSPCMeetingWork
    {
        public long NSPCMeetingWorkId { get; set; }
        public string FileNo { get; set; }
        public string FileUrl { get; set; }
        public long CategoryId { get; set; }
        public string OfficerName { get; set; }
        public string DescriptionBody { get; set; }
        public string ToAuthority { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public string CategoryName { get; set; }
    }
}
