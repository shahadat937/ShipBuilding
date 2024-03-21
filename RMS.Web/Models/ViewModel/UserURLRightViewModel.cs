using System.Collections.Generic;
using System.Web.Mvc;
using RMS.Model;
using RMS.Utility;

namespace RMS.Web.Models.ViewModel
{
    public class UserURLRightViewModel:AppCommonInfo
    {
        public UserURLRight UserUrlRight { get; set; }
        public List<UserURLRight> UserURLRights { get; set; }
        public List<Role> Roles { get; set; } 
        public UserURLRightViewModel()
        {
            UserUrlRight = new UserURLRight();
            UserURLRights = new List<UserURLRight>();
            Roles = new List<Role>();
        }

        public IEnumerable<SelectListItem> RoleSelectedItemList
        {
            get { return new SelectList(Roles, "RoleName", "RoleName"); }
        } 
    }
}