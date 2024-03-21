using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
  public class ContractSign
    {
        public int id { get; set; }
        public long Demandid { get; set; }
        public string Reference { get; set; }
        public string SendName { get; set; }
        public Nullable<System.DateTime> SendDate { get; set; }
        public string FilePath { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
        public virtual Demand Demand { get; set; }
    }
}
