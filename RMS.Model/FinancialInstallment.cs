using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class FinancialInstallment
    {
        public FinancialInstallment()
        {
           
            this.ProjectInstallments = new HashSet<ProjectInstallment>();
        }

        public long FinancialInstallmentId { get; set; }
        public long ProjectId { get; set; }
        public int FinancialYearId { get; set; }

        public virtual BGPG BGPG { get; set; }
        public virtual Demand Demand { get; set; }
        public virtual FinancialYear FinancialYear { get; set; }
        public virtual ICollection<ProjectInstallment> ProjectInstallments { get; set; }

        public Nullable<int> Percentage { get; set; }
        public Nullable<decimal> Amount { get; set; }
       
    }
}
