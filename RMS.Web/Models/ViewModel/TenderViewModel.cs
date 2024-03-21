using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.BLL.Manager;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class TenderViewModel : BaseViewModel
    {
        public TenderOffer TenderOffer{ get; set; }
        public TenderSpecification TenderSecification { get; set; }
        public List<TenderSpecification> TenderSecifications { get; set; }
        public List<TenderOffer> TenderOffers { get; set; }
        public List<SelectionList> ProjectList { get; set; }
        public List<SelectionList> TenderingType { get; set; }
        public Dictionary<string, TenderSpecificationChild> TenderChildDictionary { get; set; }
        public TenderViewModel()
        {
            TenderOffer = new TenderOffer();
            TenderSecification =new TenderSpecification();
            TenderSecifications =new List<TenderSpecification>();
            TenderOffers = new List<TenderOffer>();
            ProjectList =new List<SelectionList>();
            TenderingType =new List<SelectionList>();
            TenderChildDictionary =new Dictionary<string, TenderSpecificationChild>();
        }
        public string SearchKey { get; set; }
        public string FlowSerial { get; set; }
        public string Key { get; set; }
        public IEnumerable<SelectListItem> ProjectSelectList
        {
            get
            {
                return new SelectList(ProjectList, "Value", "Text");
            }
        }
        public IEnumerable<SelectListItem> TenderingSelectListItems
        {
            get
            {
                return new SelectList(TenderingType, "Value", "Text");
            }
        }

        [DataType(DataType.Upload)]
      
        public HttpPostedFileBase ImageUpload { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ApproveDocument { get; set; }
    }
}