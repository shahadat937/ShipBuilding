using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class ContractViewModel
    {
        public ContractViewModel()
        {
            Contract = new Contract();
            Contracts = new List<Contract>();
        }
        public Contract Contract { set; get; }
        public List<Contract> Contracts { set; get; }
        public string Message { set; get; }
        public string SearchKey { set; get; }



        public List<SelectionList> ContractFieldSelectedItems { get; set; }

        public List<SelectListItem> ContractFileSelectedItems { get; set; }
    }
}