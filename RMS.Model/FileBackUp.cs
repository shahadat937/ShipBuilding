using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class FileBackUp
    {
        public long Id { get; set; }
        public Nullable<long> FormId { get; set; }
        public string FileUrl { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }

        public virtual FormInfo FormInfo { get; set; }
    }
}
