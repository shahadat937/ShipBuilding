using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class TenderSpecDeptOpinionViewModel
    {
        public TenderSpecDeptOpinionViewModel()
        {
            TenderSpecDeptOpinion = new TenderSpecDeptOpinion();
            TenderSpecDeptOpinions = new List<TenderSpecDeptOpinion>();
            FileSelectedItems = new List<SelectionList>();
            StaffRequirementDeptOpinions =new List<StaffRequirementDeptOpinion>();
            ContractDeptOpinions =new List<ContractDeptOpinion>();
        }
        public TenderSpecDeptOpinion TenderSpecDeptOpinion { set; get; }
        public List<TenderSpecDeptOpinion> TenderSpecDeptOpinions { set; get; }
        public List<StaffRequirementDeptOpinion> StaffRequirementDeptOpinions { get; set; }
        public List<ContractDeptOpinion> ContractDeptOpinions { get; set; }
        public List<SelectListItem> DepartmentSelectedItems { set; get; }
        public List<SelectionList> FileSelectedItems { set; get; }
        public string Message { get; set; }
        public string SearchKey { get; set; }
        public string FlowSerial { get; set; }
        public string FlowName { get; set; }
        public string FormName { get; set; }
        public string FormId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public IEnumerable<SelectListItem> ProjectSelectListItems
        {
            get
            {
                return new SelectList(FileSelectedItems, "Value", "Text");
            }
        }
    }
}