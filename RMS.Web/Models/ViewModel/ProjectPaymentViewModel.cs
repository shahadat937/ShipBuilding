using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class ProjectPaymentViewModel
    {
        public ProjectPaymentViewModel()
        {
          
            Demands = new List<Demand>();
            Demand = new Demand();
            BGPG =new BGPG();
            FinancialInstallment =new FinancialInstallment();
        }
        public FinancialInstallment FinancialInstallment { get; set; }
        public BGPG BGPG { get; set; } 
        public Demand Demand { set; get; }
        public List<Demand> Demands{set;get;}
        public IEnumerable<SelectListItem>ProjectPaymentSeletedList
        {
            get { return new SelectList(Demands, "DemandId", "FileNO"); }
        }
        public string Message{set;get;}

        public Nullable<decimal> Amount { get; set; }
        

    }
}