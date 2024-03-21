using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class PaymentSchedule
    {
        public long Id { get; set; }
        public string FileNo { get; set; }
        public Nullable<decimal> BudgetAmount { get; set; }
        public Nullable<long> PaymentType { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateId { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateId { get; set; }
    }
}
