using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class FinancialInstallmentViewModel : BaseViewModel
    {
        public FinancialInstallmentViewModel()
        {
            FinancialYear = new FinancialYear();
            FinancialYears = new List<FinancialYear>();
        }
        public FinancialYear FinancialYear { get; set; }
        public List<FinancialYear> FinancialYears { get; set; }
        public string SearchKey { get; set; }
    } 
}