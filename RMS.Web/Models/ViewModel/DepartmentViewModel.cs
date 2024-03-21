using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Web.Models.ViewModel
{
    public class DepartmentViewModel : BaseViewModel
    {
        public DepartmentViewModel()
        {
            Department=new Department();
            Departments=new List<Department>();
        }
        public Department Department { get; set; }
        public List<Department> Departments { get; set; }
        public string SearchKey { get; set; }
    }
}