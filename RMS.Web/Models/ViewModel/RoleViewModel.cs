using System.Collections.Generic;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class RoleViewModel : AppCommonInfo
    {
        public int Count { get; set; }
        public Role Role { get; set; }
        public List<Role> Roles { get; set; }
        public RoleViewModel()
        {
            Role = new Role();
            Roles = new List<Role>();
        }
    }
}