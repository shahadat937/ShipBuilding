using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class ContractSignViewModel
    {
        public ContractSign ContractSign { get; set; }
        public List<ContractSign> ContractSigns { get; set; }
        public List<SelectionList> projectLists { get; set; }
        public string SearchKey { get; set; }
        public string SuccessMessage { get; set; }
        public string FailedMessage { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase DemandingUpload { get; set; }
        public ContractSignViewModel()
        {
            ContractSign = new ContractSign();
            ContractSigns = new List<ContractSign>();
            projectLists = new List<SelectionList>();
        }
        public IEnumerable<SelectListItem> ProjectSelectListItems
        {
            get
            {
                return new SelectList(projectLists, "Value", "Text");
            }
        }

        public string FlowSerial { get; set; }
        public Int64? FileId { get; set; }
    }
}