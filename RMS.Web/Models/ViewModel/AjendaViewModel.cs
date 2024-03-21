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
    public class AjendaViewModel : BaseViewModel
    {
        public Agendum Agendum { get; set; }
        public List<Agendum> Agendums { get; set; }
        public List<SelectionList> CategoryLists { get; set; } 
        public AjendaViewModel()
        {
            Agendum = new Agendum();
            Agendums = new List<Agendum>();
            CategoryLists = new List<SelectionList>();
        }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
        public string SearcKey { get; set; }
        public string ValidationCheckBySubject { get; set; }
        public IEnumerable<SelectListItem> CategorySelectListItems
        {
            get
            {
                return new SelectList(CategoryLists, "Value", "Text");
            }
        }
    }
}