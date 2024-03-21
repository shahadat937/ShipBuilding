using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
  public class CommitteNameList
    {
        public CommitteNameList()
        {
            this.CommitteInfoes = new HashSet<CommitteInfo>();
        }

        public long Id { get; set; }
        public string CommitteName { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateId { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }

        public virtual ICollection<CommitteInfo> CommitteInfoes { get; set; }
    }
}
