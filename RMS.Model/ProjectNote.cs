using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class ProjectNote
    {
        public int Id { get; set; }
        public long DemandId { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public Nullable<int> Complete { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }

        public virtual Demand Demand { get; set; }
    }
}
