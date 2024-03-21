using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectInstallment
    {
        public long InstallmentId { get; set; }
        public Nullable<long> FinancialInstallmentId { get; set; }
        public long InstallmentNo { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> Percent { get; set; }
        public string Term { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<long> Status { get; set; }

        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public virtual CommonCode CommonCode { get; set; }
        public virtual FinancialInstallment FinancialInstallment { get; set; }

        
    }
}
