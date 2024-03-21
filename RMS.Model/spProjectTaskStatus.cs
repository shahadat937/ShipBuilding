using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Model
{
   public class spProjectTaskStatus
    {
       public Nullable<int> Progress { get; set; }
       public string DemandingUrl { get; set; }
       public string FileUrl { get; set; }
        public string FileNo { get; set; }
        public string ChildOrganization { get; set; }
        public string TenderTypeFile { get; set; }
        public string PreviousDate { get; set; }
        public string DeliveryDate { get; set; }
        public string TermsOfPayment { get; set; }
        public string LocalAgent { get; set; }
        public string TotalContractPrice { get; set; }
        public string ForeignPrinciple { get; set; }
        public string Organization { get; set; }
        public string ControlName { get; set; }
        public string StaffTitle { get; set; }
        public string StaffPDF { get; set; }
        public string StaffRef { get; set; }
        public string StaffApproveFile { get; set; }
        public string StaffDecition { get; set; }
        public string TenderRef { get; set; }
        public string Builder { get; set; }
        public string TenderApprove { get; set; }
        public string TenderDecition { get; set; }
        public string tenderStatus { get; set; }
        public string TenderFile { get; set; }
        public Nullable<long> TenderType { get; set; }
        public string AfdLettetStatus { get; set; }
        public string AfdLetter { get; set; }
        public string TEA { get; set; }
        public string FATSchedule { get; set; }
        public string FATProcedure { get; set; }
        public string FATReport { get; set; }
        public string HATSAT { get; set; }
        public List<TenderSpecificationChild> TenderSpecificationChilds { get; set; } 
    }
}
