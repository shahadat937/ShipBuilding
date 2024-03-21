using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class TenderSpecificationChild
    {
        public int Id { get; set; }
        public long ProjectId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public bool IsChecked { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }

        public virtual Demand Demand { get; set; }
    }
}
