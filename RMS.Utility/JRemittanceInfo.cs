using System;
using System.ComponentModel;

namespace RMS.Utility
{
    public class JRemittanceInfo
    {
        public string IssuingBranch { get; set; }
        public string IssuingBranchName { get; set; }
        public string IssuingCountry { get; set; }
        public DateTime DateofPayment { get; set; }
        public string FileDate { get; set; }
        public string FileName { get; set; }
        public Int32 NoOfRemittance { get; set; }
        public decimal FAmount { get; set; }
        public string PaymentType { get; set; }
        
    }
}
