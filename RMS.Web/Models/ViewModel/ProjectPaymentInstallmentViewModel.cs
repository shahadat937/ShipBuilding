using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RMS.Web.Models.ViewModel
{
    public class ProjectPaymentInstallmentViewModel
    {
        public ProjectPaymentInstallmentViewModel()
        {
            ProjectInstallment = new ProjectInstallment();
            ProjectInstallments = new List<ProjectInstallment>();
            this.ProjectInstallmentDictionary = new Dictionary<string, ProjectInstallment>();
            CommonCodes = new List<CommonCode>();
            Demands=new List<Demand>();
            FinancialYears =new List<FinancialYear>();
            FinancialInstallments =new List<FinancialInstallment>();
           
            FinancialInstallment = new FinancialInstallment();
            FinancialInstallmentDictionary =new Dictionary<string, FinancialInstallment>();
            Demand =new Demand();
        }
        public string Key { get; set; }

        public Demand Demand { get; set; }
        public FinancialInstallment FinancialInstallment { get; set; }
        public List<FinancialInstallment> FinancialInstallments { get; set; } 
        public List<CommonCode> CommonCodes { set; get; }
        public List<Demand>Demands{set;get;}
        public List<FinancialYear> FinancialYears { get; set; } 
        public ProjectInstallment ProjectInstallment { set; get; }
        public List<ProjectInstallment> ProjectInstallments { set; get; }
        public Dictionary<string, ProjectInstallment> ProjectInstallmentDictionary { set; get; }
        public Dictionary<string, FinancialInstallment> FinancialInstallmentDictionary { set; get; }
        public string Message { set; get; }
        public IEnumerable<SelectListItem> ProjectPaymentInstallment
        {
            get { return new SelectList(CommonCodes, "CommonCodeId", "TypeValue"); }
        }
        public IEnumerable<SelectListItem> FinancialYearSelectListItems
        {
            get { return new SelectList(FinancialYears, "Id", "Value"); }
        }
        public IEnumerable<SelectListItem> ProjectSelectedList
        {
            get { return new SelectList(Demands, "DemandId", "FileNo"); }
        }
    }
}