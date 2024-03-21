using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Model;

namespace RMS.Web.Models.ViewModel
{
    public class FinancialMinistryViewModel : BaseViewModel
    {
        public FinanceMinistryLetter FinanceMinistryLetter { get; set; }
        public List<FinanceMinistryLetter> FinanceMinistryLetters { get; set; }
        public List<Department> Departments { get; set; } 
        public FinancialMinistryViewModel()
        {
            FinanceMinistryLetter = new FinanceMinistryLetter();
            FinanceMinistryLetters = new List<FinanceMinistryLetter>();
            Departments = new List<Department>();
        }
        public string SearchKey { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        public IEnumerable<SelectListItem> DeptSelectListItems
        {
            get
            {
                return new SelectList(Departments, "DepartmentId", "Name");
            }
        }
    }
}