using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    public class Contract
    {
        public long ContractId { get; set; }
        public Nullable<long> ContractFileId { get; set; }
        public Nullable<long> FieldId { get; set; }
        public string FieldValue { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual ContractField ContractField { get; set; }
        public virtual ContractFile ContractFile { get; set; }

        public string ContractFileName { set; get; }
        public string FieldName { set; get; }
    }
}
