using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Web.Models.ViewModel
{
    public class FormInfoViewModel
    {
        public FormInfoViewModel()
        {
            FormInfoes = new List<FormInfo>();
            FormInfo=new FormInfo();
        }
        public FormInfo FormInfo { set; get; }
        public List<FormInfo> FormInfoes {set;get;}
        public IEnumerable<SelectListItem> Forms
        {
            get
            {
                return new SelectList(FormInfoes, "FormId", "FormName");
            }
        }

        public string Message { get; set; }
    }
}