using System.Web.Mvc;
using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class ContractFieldViewModel
    {
        public ContractFieldViewModel()
        {
            ContractField = new ContractField();
            ContractFields = new List<ContractField>();
            this.ContractFieldDictionary = new Dictionary<string, ContractField>();
            ContractFieldItem = new List<SelectionList>();
        }
        public string Key { get; set; }
        public ContractField ContractField { set; get; }
        public List<SelectionList> ContractFieldItem { get; set; }
        public List<ContractField> ContractFields { set; get; }
        public Dictionary<string, ContractField> ContractFieldDictionary { get; set; }
        public string Message { set; get; }
        public string SearchKey { set; get; }
        public long FlowSerialId { get; set; }
        public IEnumerable<SelectListItem> ContractFieldSelectListItems
        {
            get
            {
                return new SelectList(ContractFieldItem, "Value", "Text");
            }
        }
    }
}