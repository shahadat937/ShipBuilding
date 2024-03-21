using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class FileTreeList
    {
        public Nullable<long> ProjectId { get; set; }
        public Nullable<long> ParentCode { get; set; }
        public Nullable<long> FileId { get; set; }
        public string FileName { get; set; }
        public string FormURL { get; set; }
        public string EntryStatus { get; set; }
        public string Level { get; set; }
        public Nullable<int> FlowSerial { get; set; }
        public string taskSerial { get; set; }
        public Nullable<bool> IsSkip { get; set; }

       // Other Property
        public Nullable<bool> status { get; set; }
        public string FormId { get; set; }
    }
}
