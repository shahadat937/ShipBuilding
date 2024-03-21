using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Web.CustomModel
{
    public class NextSubMenu
    {
        public string MenuId { get; set; }
        public System.Guid ApplicationId { get; set; }
        public string UserId { get; set; }
        public string MenuObjectName { get; set; }
        public string MenuObjectDescription { get; set; }
        public string UrlLink { get; set; }
        public Nullable<bool> AddInfo { get; set; }
        public Nullable<bool> ChangeInfo { get; set; }
        public Nullable<bool> DeleteInfo { get; set; }
        public Nullable<bool> ExecuteProc { get; set; }
        public Nullable<bool> Visible { get; set; }
        public string TableName { get; set; }
        public string SPName { get; set; }
        public string GroupName { get; set; }
        public string MenuLevel { get; set; }
        public string FirstLevel { get; set; }
        public string SecondLevel { get; set; }
        public string ThirdLevel { get; set; }
        public System.DateTime LastUpdate { get; set; }
    }
}