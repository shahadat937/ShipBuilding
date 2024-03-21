using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class BGPG
    {
        public int GuranteeId { get; set; }
        public long FinancialInstallmentId { get; set; }
        public Nullable<decimal> GuranteeAmount { get; set; }
        public Nullable<System.DateTime> GuranteeDate { get; set; }

        public virtual FinancialInstallment FinancialInstallment { get; set; }
    }
}
