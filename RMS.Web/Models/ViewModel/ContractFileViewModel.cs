using RMS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class ContractFileViewModel
    {
        public ContractFileViewModel()
        {
            ContractFile = new ContractFile();
            ContractFiles = new List<ContractFile>();
            DemandSelectedItems = new List<SelectionList>();
        }
        public ContractFile ContractFile { set; get; }
        public List<ContractFile> ContractFiles { set; get; }
        public string Message { set; get; }
        public string SearchKey { set; get; }
        public List<SelectionList> DemandSelectedItems { set; get; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ApproveDocument { get; set; }
        public string FileSerial { get; set; }

        public IEnumerable<SelectListItem> DemandSelectListItems
        {
            get
            {
                return new SelectList(DemandSelectedItems, "Value", "Text");
            }
        }
    }
}