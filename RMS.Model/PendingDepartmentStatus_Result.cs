using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class PendingDepartmentStatus_Result
    {
        public Nullable<long> RowNumber { get; set; }
        public int ProjectId { get; set; }
        public string DepartmentId { get; set; }
        public string FormId { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string EntryStatus { get; set; }
        public string Level { get; set; }
        public Nullable<int> FlowSerial { get; set; }
        public string TaskSerial { get; set; }
        public Nullable<bool> IsSkip { get; set; }
        public Nullable<System.DateTime> DepEndDate { get; set; }
    }
}
