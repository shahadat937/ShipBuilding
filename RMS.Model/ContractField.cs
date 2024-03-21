using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;

    public partial class ContractField
    {
        public long FieldId { get; set; }
        public Nullable<long> FieldName { get; set; }
        public string FieldValue { get; set; }
        public long ContractFileId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual CommonCode CommonCode { get; set; }
    }
}
