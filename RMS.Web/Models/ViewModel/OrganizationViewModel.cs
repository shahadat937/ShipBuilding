using RMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Web.Models.ViewModel
{
    public class OrganizationViewModel
    {
        public OrganizationViewModel()
        {
            Organization = new Organization();
            Organizations = new List<Organization>();
        }
        public Organization Organization { set; get; }
        public List<Organization> Organizations { set; get; }
        public string Message { set; get; }
        public string SearchKey { set; get; }
    }
}